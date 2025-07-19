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
                        callback(obj, ctx);
                });
            }

            return contract;
        }

        private static Action<object, StreamingContext>? CreateValidationCallback(Type objectType)
        {
            var validatable = objectType
                .GetInterfaces()
                .FirstOrDefault(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IValidatable<>));
            if (validatable == null)
                return null;

            // obtém método Validate() e as props IsSuccess/Errors
            var validateMethod = validatable.GetMethod("Validate", BindingFlags.Public | BindingFlags.Instance)!;
            var resultType = validateMethod.ReturnType;
            var isSuccessProp = resultType.GetProperty("IsSuccess", BindingFlags.Public | BindingFlags.Instance)!;
            var errorsProp = resultType.GetProperty("Errors", BindingFlags.Public | BindingFlags.Instance)!;

            // parâmetros do callback
            var paramObj = Expression.Parameter(typeof(object), "obj");
            var paramCtx = Expression.Parameter(typeof(StreamingContext), "ctx");

            // chama Validate()
            var castObj = Expression.Convert(paramObj, objectType);
            var callValidate = Expression.Call(castObj, validateMethod);
            var isSuccess = Expression.Property(callValidate, isSuccessProp);

            // pega Errors como IEnumerable<ResultError>
            var errorsExpr = Expression.Property(callValidate, errorsProp);
            var errorsEnumerable = Expression.Convert(errorsExpr, typeof(IEnumerable<ResultError>));

            // chama nosso helper ThrowErrors(errorsEnumerable)
            var throwHelper = typeof(ValidatingContractResolver)
                .GetMethod(nameof(ThrowErrors), BindingFlags.NonPublic | BindingFlags.Static)!;
            var callThrow = Expression.Call(throwHelper, errorsEnumerable, paramCtx, paramObj);

            // if (!IsSuccess) ThrowErrors(...)
            var ifThen = Expression.IfThen(
                Expression.IsFalse(isSuccess),
                callThrow
            );

            var lambda = Expression.Lambda<Action<object, StreamingContext>>(ifThen, paramObj, paramCtx);
            return lambda.Compile();
        }

        // Lança JsonSerializationException usando cada ResultError.PropertyName como path
        private static void ThrowErrors(IEnumerable<ResultError> errors, StreamingContext paramCtx, object paramObj)
        {
            foreach (var err in errors)
            {
                throw new JsonSerializationException(
                    err.Message,
                    err.PropertyName ?? string.Empty, // path
                    0,                                  // lineNumber
                    0,                                  // linePosition
                    null                                // innerException
                );
            }
        }
    }
}
