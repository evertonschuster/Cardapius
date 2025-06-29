namespace BuildingBlock.Domain.ValueObjects
{
    public readonly struct Result<T>
    {
        public const string DefaultValidationError = "Erro desconhecido de validação.";

        public T? Value { get; }
        public IReadOnlyList<string> Errors { get; }
        public bool IsSuccess => Errors.Count == 0;

        private Result(T? value, IReadOnlyList<string> errors)
        {
            Value = value;
            Errors = errors ?? [];
        }

        public static Result<T> Success(T value) => new(value, []);

        public static Result<T> Fail(params string[] errors)
        {
            return new(
                default,
                errors != null && errors.Length > 0
                    ? (IReadOnlyList<string>)errors
                    : []
            );
        }

        public static Result<T> Fail(IEnumerable<string> errors)
        {
            if (errors is IReadOnlyList<string> list)
            {
                return new(default, list);
            }

            var asArray = errors?.ToArray() ?? [];
            return new(default, asArray);
        }

        internal static Result<T> FromValidation(ValidationResult result, Func<T> valueFactory)
        {
            if (result.IsValid)
                return Success(valueFactory());

            var errs = result.Errors ?? [DefaultValidationError];
            return Fail(errs);
        }

        internal static Result<T> FromValidation(ValidationResult result, T value)
        {
            if (result.IsValid)
                return Success(value);

            var errs = result.Errors ?? [DefaultValidationError];
            return Fail(errs);
        }
    }
}