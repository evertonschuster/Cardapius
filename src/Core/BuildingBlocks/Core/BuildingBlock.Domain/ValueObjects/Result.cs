namespace BuildingBlock.Domain.ValueObjects
{
    public readonly struct Result
    {
        public const string DefaultValidationError = "Erro desconhecido de validação.";

        public IReadOnlyList<ResultError> Errors { get; } = [];
        public bool IsSuccess => Errors.Count == 0;

        private Result(IReadOnlyList<ResultError> errors)
        {
            Errors = errors ?? [];
        }

        public static Result Success() => new([]);

        public static Result Fail(params ResultError[] errors)
        {
            return new(
                errors != null && errors.Length > 0 ? errors as IReadOnlyList<ResultError> : []
            );
        }

        public static Result Fail(string PropertyName, params string[] message)
        {
            var errors = message?
                .Select(m => new ResultError(PropertyName, m))
                .ToArray()
                ?? [];
            return new(errors);
        }
    }

    public readonly struct Result<T>
    {
        public const string DefaultValidationError = "Erro desconhecido de validação.";

        public T? Value { get; }
        public IReadOnlyList<ResultError> Errors { get; } = [];
        public bool IsSuccess => Errors.Count == 0;

        private Result(T? value, IReadOnlyList<ResultError> errors)
        {
            Value = value;
            Errors = errors ?? [];
        }

        public static Result<T> Success(T value) => new(value, []);

        public static Result<T> Fail(params ResultError[] errors)
        {
            return new(
                default, errors != null && errors.Length > 0 ? errors as IReadOnlyList<ResultError> : []
            );
        }

        public static Result<T> Fail(IEnumerable<ResultError> errors)
        {
            if (errors is IReadOnlyList<ResultError> list)
            {
                return new(default, list);
            }

            var asArray = errors?.ToArray() ?? [];
            return new(default, asArray);
        }

        public static Result<T> Fail(string PropertyName, string message)
        {
            return new(default, [new ResultError(PropertyName, message)]);
        }

        internal static Result<T> FromValidation(ValidationResult result, Func<T> valueFactory)
        {
            if (result.IsValid)
                return Success(valueFactory());

            return Fail(result.Errors ?? []);
        }

        internal static Result<T> FromValidation(ValidationResult result, T value)
        {
            if (result.IsValid)
                return Success(value);

            return Fail(result.Errors ?? []);
        }
    }

    public readonly struct ResultError
    {
        public string Message { get; }
        public string? PropertyName { get; }
        public ResultError(string? propertyName, string message)
        {
            Message = message;
            PropertyName = propertyName;
        }
        public ResultError(string message)
        {
            Message = message;
            PropertyName = null;
        }

        public override string ToString()
        {
            return PropertyName is null
                ? Message
                : $"{PropertyName}: {Message}";
        }
    }
}