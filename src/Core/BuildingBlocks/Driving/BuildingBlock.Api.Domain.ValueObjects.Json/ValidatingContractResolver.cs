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

            var castObj = Expression.Convert(paramObj, objectType);
            var callValidate = Expression.Call(castObj, validateMethod);
            var isSuccess = Expression.Property(callValidate, isSuccessProp);
            var errors = Expression.Convert(
                Expression.Property(callValidate, errorsProp),
                typeof(IReadOnlyList<string>));

            var joinMethod = typeof(string).GetMethod(
                nameof(string.Join),
                [typeof(string), typeof(IEnumerable<string>)]
            )!;
            var errorMessage = Expression.Call(
                joinMethod,
                Expression.Constant(", "),
                errors);

            var exceptionCtor = typeof(JsonSerializationException)
                .GetConstructor([typeof(string)])!;
            var throwExpr = Expression.Throw(
                Expression.New(exceptionCtor, errorMessage),
                typeof(void)
            );

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
