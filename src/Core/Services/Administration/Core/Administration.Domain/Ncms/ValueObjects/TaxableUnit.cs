namespace Administration.Domain.Ncms.ValueObjects
{
    public record TaxableUnit : ValueObject
    {
        public string Unit { get; init; }
        public string Description { get; init; }

        public TaxableUnit(string unit, string description)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException("Unidade tributada é obrigatória.", nameof(unit));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Descrição da unidade tributada é obrigatória.", nameof(description));
            }

            Unit = unit;
            Description = description;
        }
    }
}
