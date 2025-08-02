namespace BuildingBlock.Domain.ValueObjects.Time
{
    internal static class PreparationTimeValidator
    {
        private const string EmptyError = "O tempo de preparação deve ser informado.";
        private const string NegativeError = "O tempo de preparação não pode ser negativo.";

        public static ValidationResult Validate(TimeSpan? value)
        {
            if (!value.HasValue)
                return ValidationResult.Failure(errors: EmptyError);

            if (value.Value < TimeSpan.Zero)
                return ValidationResult.Failure(errors: NegativeError);

            return ValidationResult.Success();
        }
    }

}
