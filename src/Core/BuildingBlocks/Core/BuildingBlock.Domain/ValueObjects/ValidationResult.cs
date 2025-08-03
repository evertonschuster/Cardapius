using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects;

/// <summary>
/// Representa o resultado de uma operação de validação de domínio.
/// </summary>
public record ValidationResult
{
    public bool IsValid => Errors == null || Errors.Count == 0;
    public bool IsFailure => !IsValid;

    public IReadOnlyList<ResultError>? Errors { get; }

    public string? FirstError
    {
        get
        {
            if (Errors?.Any() == true)
                return Errors[0].Message;

            return null;
        }
    }

    /// <summary>
    /// Returns a single string containing all error messages, separated by commas, or null if there are no errors.
    /// </summary>
    /// <returns>A concatenated string of all error messages, or null if no errors exist.</returns>
    public string? GetAllMessages()
    {
        if (Errors == null || Errors.Count == 0)
            return null;
        return string.Join(", ", Errors.Select(e => e.Message));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class with the specified collection of validation errors.
    /// </summary>
    /// <param name="errors">A collection of <see cref="ResultError"/> objects representing validation errors.</param>
    protected ValidationResult(IEnumerable<ResultError> errors)
    {
        Errors = errors.ToList().AsReadOnly();
    }

    /// <summary>
    /// Throws a <see cref="BusinessException"/> with the first error message if the validation result is a failure.
    /// </summary>
    public void ThrowIfInvalid()
    {
        if (IsFailure)
        {
            throw new BusinessException(FirstError ?? "Erro desconhecido de validação.");
        }
    }

    /// <summary>
    /// Creates a successful validation result with no errors.
    /// </summary>
    /// <returns>A <see cref="ValidationResult"/> indicating success.</returns>
    public static ValidationResult Success() => new([]);

    /// <summary>
    /// Creates a failed <see cref="ValidationResult"/> with errors associated to a specific field.
    /// </summary>
    /// <param name="field">The name of the field related to the validation errors.</param>
    /// <param name="errors">A collection of error messages for the specified field.</param>
    /// <returns>A <see cref="ValidationResult"/> representing failure with the provided field errors.</returns>
    public static ValidationResult Failure(string field, params IEnumerable<string> errors)
    {

        if (errors == null || !errors.Any())
        {
#if DEBUG
            throw new BusinessException("Erro desconhecido de validação.");
#else
            errors = ["Erro desconhecido de validação."];
#endif
        }

        var fieldErrors = errors.Select(error => new ResultError(field, error));
        return new(fieldErrors);
    }

    /// <summary>
    /// Creates a failed <see cref="ValidationResult"/> with the specified error messages not associated with any field.
    /// </summary>
    /// <param name="errors">A collection of error messages describing the validation failures.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the provided errors.</returns>
    public static ValidationResult Failure(params IEnumerable<string> errors)
    {
        if (errors == null || !errors.Any())
        {
#if DEBUG
            throw new BusinessException("Erro desconhecido de validação.");
#else
            errors = ["Erro desconhecido de validação."];
#endif
        }

        var fieldErrors = errors.Select(error => new ResultError(null, error));
        return new(fieldErrors);
    }
}


/// <summary>
/// Representa um erro de validação.
/// </summary>
public sealed record ValidationError
{
    public BusinessException Exception { get; }

    public string Message => Exception.Message;

    public ValidationError(BusinessException exception)
    {
        Exception = exception ?? throw new BusinessException("Erro desconhecido de validação.");
    }

    public static ValidationError FromMessage(string message) =>
        new(new BusinessException(message));
}
