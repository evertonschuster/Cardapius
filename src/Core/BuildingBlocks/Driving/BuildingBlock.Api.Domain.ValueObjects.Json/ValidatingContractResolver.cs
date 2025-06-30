using BuildingBlock.Domain.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace BuildingBlock.Api.Domain.ValueObjects.Json
{
    internal class ValidatingContractResolver : DefaultContractResolver
    {
        private readonly ConcurrentDictionary<Type, Action<object, StreamingContext>?> _validationCallbacks
            = new ConcurrentDictionary<Type, Action<object, StreamingContext>?>();

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);

            var callback = _validationCallbacks.GetOrAdd(objectType, CreateValidationCallback);

            if (callback != null)
            {
                contract.OnDeserializedCallbacks.Add((obj, ctx) =>
                {
                    if (obj != null)
                    {
                        callback(obj, ctx);
                    }
                });
            }

            return contract;
        }

        private static Action<object, StreamingContext>? CreateValidationCallback(Type objectType)
        {
            var validatableInterface = objectType
                .GetInterfaces()
                .FirstOrDefault(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IValidatable<>));
            if (validatableInterface == null)
                return null;

            var validateMethod = validatableInterface.GetMethod("Validate", BindingFlags.Public | BindingFlags.Instance)!;
            var resultType = validateMethod.ReturnType;
            var isSuccessProp = resultType.GetProperty("IsSuccess", BindingFlags.Public | BindingFlags.Instance)!;
            var errorsProp = resultType.GetProperty("Errors", BindingFlags.Public | BindingFlags.Instance)!;

            var paramObj = Expression.Parameter(typeof(object), "obj");
            var paramCtx = Expression.Parameter(typeof(StreamingContext), "ctx");

            // (T)obj
            var castObj = Expression.Convert(paramObj, objectType);
            // ((IValidatable<T>)obj).Validate()
            var callValidate = Expression.Call(castObj, validateMethod);
            // result.IsSuccess
            var isSuccess = Expression.Property(callValidate, isSuccessProp);

            // result.Errors  (IReadOnlyList<ResultError>)
            var errorsExpr = Expression.Property(callValidate, errorsProp);
            // converte para IEnumerable<ResultError>
            var errorsEnumerable = Expression.Convert(errorsExpr, typeof(IEnumerable<ResultError>));

            // Enumerable.Select<ResultError,string>(errors, err => err.ToString())
            var selectMethod = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == nameof(Enumerable.Select) && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(ResultError), typeof(string));
            var errParam = Expression.Parameter(typeof(ResultError), "err");
            var toStringCall = Expression.Call(errParam, typeof(object).GetMethod(nameof(ToString))!);
            var selector = Expression.Lambda<Func<ResultError, string>>(toStringCall, errParam);
            var projected = Expression.Call(selectMethod, errorsEnumerable, selector);

            // string.Join(", ", projectedErrors)
            var joinMethod = typeof(string).GetMethod(
                nameof(string.Join),
                new[] { typeof(string), typeof(IEnumerable<string>) }
            )!;
            var errorMessage = Expression.Call(joinMethod, Expression.Constant(", "), projected);

            // throw new JsonSerializationException(errorMessage)
            var exCtor = typeof(JsonSerializationException)
                .GetConstructor(new[] { typeof(string) })!;
            var throwExpr = Expression.Throw(
                Expression.New(exCtor, errorMessage),
                typeof(void)
            );

            // if (!result.IsSuccess) throw ...
            var ifThen = Expression.IfThen(
                Expression.IsFalse(isSuccess),
                throwExpr
            );

            var lambda = Expression.Lambda<Action<object, StreamingContext>>(
                ifThen, paramObj, paramCtx
            );
            return lambda.Compile();
        }

    }
}
