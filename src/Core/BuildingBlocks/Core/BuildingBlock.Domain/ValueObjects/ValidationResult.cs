using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects;

/// <summary>
/// Representa o resultado de uma operação de validação de domínio.
/// </summary>
public record ValidationResult
{
    public bool IsValid => Errors == null || Errors?.Count == 0;
    public bool IsFailure => !IsValid;

    public IReadOnlyList<ResultError>? Errors { get; }

    public string? FirstError => Errors?.FirstOrDefault().Message;

    public string? GetAllMessages()
    {
        if (Errors == null || Errors.Count == 0)
            return null;
        return string.Join(", ", Errors.Select(e => e.Message));
    }

    protected ValidationResult(IEnumerable<ResultError> errors)
    {
        Errors = errors.ToList().AsReadOnly();
    }

    public void ThrowIfInvalid()
    {
        if (IsFailure)
        {
            throw new BusinessException(FirstError ?? "Erro desconhecido de validação.");
        }
    }

    public static ValidationResult Success() => new([]);

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
