using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects
{
    /// <summary>
    /// Generic result of a validation Domain operation.
    /// </summary>
    public sealed record ValidationResult<T>
    {
        public bool IsValid { get => !Errors.Any(); }

        public T? Value { get; }

        public IReadOnlyList<ValidationError> Errors { get; }

        private ValidationResult(T value)
        {
            Value = value;
            Errors = [];
        }

        private ValidationResult(T? value, IEnumerable<ValidationError> errors)
        {
            Errors = errors.ToList().AsReadOnly();
            Value = value;
        }

        public void ThrowIfInvalid()
        {
            if (!IsValid)
                throw Errors[0].Exception;
        }

        public static ValidationResult<T> Success(T value) => new(value);
        public static ValidationResult<T> Failure(T? value, params IEnumerable<ValidationError> errors) => new(value, errors);
    }


    public sealed record ValidationError
    {
        public BusinessException Exception { get; }

        public string Message => Exception.Message;

        public ValidationError(BusinessException exception)
        {
            Exception = exception;
        }
    }
}
