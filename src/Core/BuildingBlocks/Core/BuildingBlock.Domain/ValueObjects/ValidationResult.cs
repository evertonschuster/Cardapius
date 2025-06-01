using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects;

/// <summary>
/// Representa o resultado de uma operação de validação de domínio.
/// </summary>
public record ValidationResult
{
    public bool IsValid => !Errors.Any();
    public bool IsFailure => !IsValid;

    public IReadOnlyList<string> Errors { get; }

    public string? FirstError => Errors.FirstOrDefault();

    protected ValidationResult(IEnumerable<string> errors)
    {
        Errors = errors.ToList().AsReadOnly();
    }

    public string GetMessageErrors() =>
        string.Join("\n", Errors);

    public void ThrowIfInvalid()
    {
        if (IsFailure)
        {
            throw new BusinessException(FirstError ?? "Erro desconhecido de validação.");
        }
    }

    public static ValidationResult Success() => new([]);
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

        return new(errors);
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
