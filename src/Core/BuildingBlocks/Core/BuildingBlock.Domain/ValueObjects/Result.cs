using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.ValueObjects
{
    public readonly struct Result
    {
        public const string DefaultValidationError = "Erro desconhecido de validação.";

        public IReadOnlyList<ResultError> Errors { get; } = [];
        public bool IsSuccess => Errors.Count == 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> struct with the specified list of errors.
        /// </summary>
        /// <param name="errors">A list of <see cref="ResultError"/> objects representing validation or operation errors. If null, an empty list is used.</param>
        private Result(IReadOnlyList<ResultError> errors)
        {
            Errors = errors ?? [];
        }

        /// <summary>
        /// Creates a successful <see cref="Result"/> instance with no errors.
        /// </summary>
        /// <returns>A <see cref="Result"/> indicating success.</returns>
        public static Result Success() => new([]);

        /// <summary>
        /// Creates a failed <see cref="Result"/> containing the specified errors.
        /// </summary>
        /// <param name="errors">An array of <see cref="ResultError"/> instances representing the errors. If null or empty, the result will contain no errors.</param>
        /// <returns>A <see cref="Result"/> instance representing failure with the provided errors.</returns>
        public static Result Fail(params ResultError[] errors)
        {
            return new(
                errors != null && errors.Length > 0 ? errors as IReadOnlyList<ResultError> : []
            );
        }

        /// <summary>
        /// Creates a failed <see cref="Result"/> containing one or more errors associated with the specified property name.
        /// </summary>
        /// <param name="PropertyName">The name of the property related to the errors.</param>
        /// <param name="message">One or more error messages to associate with the property.</param>
        /// <returns>A failed <see cref="Result"/> with the specified property errors.</returns>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> struct with the specified value and error list.
        /// </summary>
        /// <param name="value">The result value, or null if the operation failed.</param>
        /// <param name="errors">A list of errors associated with the result, or an empty list if none.</param>
        private Result(T? value, IReadOnlyList<ResultError> errors)
        {
            Value = value;
            Errors = errors ?? [];
        }

        /// <summary>
        /// Creates a successful <see cref="Result{T}"/> containing the specified value and no errors.
        /// </summary>
        /// <param name="value">The value to include in the successful result.</param>
        /// <returns>A <see cref="Result{T}"/> representing success with the provided value.</returns>
        public static Result<T> Success(T value) => new(value, []);

        /// <summary>
        /// Creates a failed <see cref="Result{T}"/> containing the specified errors.
        /// </summary>
        /// <param name="errors">An array of <see cref="ResultError"/> instances describing the failure reasons.</param>
        /// <returns>A failed <see cref="Result{T}"/> with the provided errors and a default value.</returns>
        public static Result<T> Fail(params ResultError[] errors)
        {
            return new(
                default, errors != null && errors.Length > 0 ? errors as IReadOnlyList<ResultError> : []
            );
        }

        /// <summary>
        /// Creates a failed <see cref="Result{T}"/> with the specified collection of errors.
        /// </summary>
        /// <param name="errors">A collection of <see cref="ResultError"/> instances describing the failure reasons.</param>
        /// <returns>A failed result containing the provided errors and a default value.</returns>
        public static Result<T> Fail(IEnumerable<ResultError> errors)
        {
            if (errors is IReadOnlyList<ResultError> list)
            {
                return new(default, list);
            }

            var asArray = errors?.ToArray() ?? [];
            return new(default, asArray);
        }

        /// <summary>
        /// Creates a failed <see cref="Result{T}"/> with a single error associated with the specified property and message.
        /// </summary>
        /// <param name="PropertyName">The name of the property related to the error.</param>
        /// <param name="message">The error message.</param>
        /// <returns>A failed result containing the specified error.</returns>
        public static Result<T> Fail(string PropertyName, string message)
        {
            return new(default, [new ResultError(PropertyName, message)]);
        }

        /// <summary>
        /// Creates a <see cref="Result{T}"/> based on a validation result, returning success with a generated value if valid, or failure with validation errors.
        /// </summary>
        /// <param name="result">The validation result to evaluate.</param>
        /// <param name="valueFactory">A function that produces the value if validation succeeds.</param>
        /// <returns>A successful result with the generated value if validation passes; otherwise, a failed result with validation errors.</returns>
        internal static Result<T> FromValidation(ValidationResult result, Func<T> valueFactory)
        {
            if (result.IsValid)
                return Success(valueFactory());

            return Fail(result.Errors ?? []);
        }

        /// <summary>
        /// Creates a <see cref="Result{T}"/> based on a validation result, returning success with the provided value if valid, or failure with validation errors.
        /// </summary>
        /// <param name="result">The validation result to evaluate.</param>
        /// <param name="value">The value to return if validation is successful.</param>
        /// <returns>A successful result containing the value if validation passes; otherwise, a failed result with validation errors.</returns>
        internal static Result<T> FromValidation(ValidationResult result, T value)
        {
            if (result.IsValid)
                return Success(value);

            return Fail(result.Errors ?? []);
        }

        internal static Result<T> FromResult(Result result, Func<T> valueFactory)
        {
            if (result.IsSuccess)
                return Success(valueFactory());

            return Fail(result.Errors ?? []);
        }
    }

    public readonly struct ResultError
    {
        public string Message { get; }
        public string? PropertyName { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultError"/> struct with the specified property name and error message.
        /// </summary>
        /// <param name="propertyName">The name of the property associated with the error, or null if not applicable.</param>
        /// <param name="message">The error message.</param>
        public ResultError(string? propertyName, string message)
        {
            Message = message;
            PropertyName = propertyName;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultError"/> struct with the specified error message and no associated property name.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ResultError(string message)
        {
            Message = message;
            PropertyName = null;
        }

        /// <summary>
        /// Returns a string representation of the error, including the property name if available.
        /// </summary>
        /// <returns>The error message, optionally prefixed by the property name.</returns>
        public override string ToString()
        {
            return PropertyName is null
                ? Message
                : $"{PropertyName}: {Message}";
        }
    }
}