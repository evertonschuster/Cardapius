using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects;

/// <summary>
/// Representa o resultado de uma operação de validação de domínio.
/// </summary>
public record ValidationResult
{
    public bool IsValid => !Errors.Any();
    public bool IsFailure => !IsValid;

    public IReadOnlyList<ValidationError> Errors { get; }

    public ValidationError? FirstError => Errors.FirstOrDefault();

    protected ValidationResult(IEnumerable<ValidationError> errors)
    {
        Errors = errors.ToList().AsReadOnly();
    }

    public string GetMessageErrors() =>
        string.Join("\n", Errors.Select(e => e.Message));

    public void ThrowIfInvalid()
    {
        if (IsFailure)
            throw FirstError?.Exception ?? new BusinessException("Erro desconhecido de validação.");
    }

    public static ValidationResult Success() => new([]);
    public static ValidationResult Failure(params IEnumerable<ValidationError> errors)
    {
        if (errors == null || !errors.Any())
        {
#if DEBUG
            throw new BusinessException("Erro desconhecido de validação.");
#else
            errors = [ValidationError.FromMessage("Erro desconhecido de validação.")];
#endif
        }

        return new(errors);
    }
}

/// <summary>
/// Representa um resultado genérico de validação contendo um valor.
/// </summary>
public sealed record ValidationResult<T> : ValidationResult
{
    public T? Value { get; }

    private ValidationResult(T value) : base([])
    {
        Value = value;
    }

    private ValidationResult(T? value, IEnumerable<ValidationError> errors) : base(errors)
    {
        Value = value;
    }

    public static ValidationResult<T> Success(T value) => new(value);

    public static ValidationResult<T> Failure(T? value, params IEnumerable<ValidationError> errors)
    {
        if (errors == null || !errors.Any())
        {
#if DEBUG
            throw new BusinessException("Erro desconhecido de validação.");
#else
            errors = [ValidationError.FromMessage("Erro desconhecido de validação.")];
#endif
        }

        return new(value, errors);
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
