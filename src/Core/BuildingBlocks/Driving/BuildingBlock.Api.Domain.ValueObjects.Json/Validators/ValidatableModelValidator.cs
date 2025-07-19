using BuildingBlock.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Validators
{
    public class ValidatableModelValidator : IModelValidator
    {
        private static readonly ConcurrentDictionary<Type, ValidatorEntry> _cache = new();

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var model = context.Model;
            if (model == null) yield break;

            var modelType = model.GetType();
            var entry = _cache.GetOrAdd(modelType, CreateValidatorEntry);

            if (entry.ValidateFunc == null)
                yield break;

            var resultObj = entry.ValidateFunc(model);
            if (!entry.IsSuccessFunc(resultObj))
            {
                foreach (var err in entry.ErrorsFunc(resultObj))
                {
                    var member = err.PropertyName ?? context.ModelMetadata.Name ?? string.Empty;
                    yield return new ModelValidationResult(member, err.Message);
                }
            }
        }

        private static ValidatorEntry CreateValidatorEntry(Type modelType)
        {
            var validInterface = modelType
                .GetInterfaces()
                .FirstOrDefault(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IValidatable<>));

            if (validInterface == null)
            {
                return new ValidatorEntry();
            }

            var validateMethod = validInterface.GetMethod(nameof(IValidatable<object>.Validate))!;
            var resultType = validateMethod.ReturnType;

            var successProp = resultType.GetProperty(nameof(Result<object>.IsSuccess))!;
            var errorsProp = resultType.GetProperty(nameof(Result<object>.Errors))!;

            var modelParam = Expression.Parameter(typeof(object), "model");
            var castModel = Expression.Convert(modelParam, modelType);
            var callValidate = Expression.Call(castModel, validateMethod);
            var castResultObj = Expression.Convert(callValidate, typeof(object));
            var validateLambda = Expression
                .Lambda<Func<object, object>>(castResultObj, modelParam)
                .Compile();

            var resultParam = Expression.Parameter(typeof(object), "result");
            var castResult = Expression.Convert(resultParam, resultType);
            var getSuccess = Expression.Property(castResult, successProp);
            var successLambda = Expression
                .Lambda<Func<object, bool>>(getSuccess, resultParam)
                .Compile();

            var getErrors = Expression.Property(castResult, errorsProp);
            var errorsConvert = Expression.Convert(getErrors, typeof(IEnumerable<ResultError>));
            var errorsLambda = Expression
                .Lambda<Func<object, IEnumerable<ResultError>>>(errorsConvert, resultParam)
                .Compile();

            return new ValidatorEntry
            {
                ValidateFunc = validateLambda,
                IsSuccessFunc = successLambda,
                ErrorsFunc = errorsLambda
            };
        }

        private sealed class ValidatorEntry
        {
            public Func<object, object>? ValidateFunc { get; set; }
            public Func<object, bool> IsSuccessFunc { get; set; } = _ => true;
            public Func<object, IEnumerable<ResultError>> ErrorsFunc
            { get; set; } = _ => [];
        }
    }
}
