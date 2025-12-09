namespace Administration.Domain.Ncms.ValueObjects
{
    public record TaxEstimation : ValueObject
    {
        public decimal National { get; init; }
        public decimal Imported { get; init; }
        public decimal State { get; init; }
        public decimal Municipal { get; init; }

        public TaxEstimation(decimal national, decimal imported, decimal state, decimal municipal)
        {
            ValidateRate(national, nameof(national));
            ValidateRate(imported, nameof(imported));
            ValidateRate(state, nameof(state));
            ValidateRate(municipal, nameof(municipal));

            National = national;
            Imported = imported;
            State = state;
            Municipal = municipal;
        }

        private static void ValidateRate(decimal value, string propertyName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(propertyName, value, "Estimativa de tributo não pode ser negativa.");
            }
        }
    }
}
