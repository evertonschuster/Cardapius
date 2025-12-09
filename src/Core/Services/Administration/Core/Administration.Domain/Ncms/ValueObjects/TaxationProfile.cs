namespace Administration.Domain.Ncms.ValueObjects
{
    public record TaxationProfile : ValueObject
    {
        public string TaxationDescription { get; init; }
        public decimal? FixedRate { get; init; }

        public TaxationProfile(string taxationDescription, decimal? fixedRate)
        {
            if (string.IsNullOrWhiteSpace(taxationDescription))
            {
                throw new ArgumentException("Descrição de tributação é obrigatória.", nameof(taxationDescription));
            }

            if (fixedRate.HasValue && fixedRate.Value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fixedRate), fixedRate, "Alíquota fixa não pode ser negativa.");
            }

            TaxationDescription = taxationDescription;
            FixedRate = fixedRate;
        }
    }
}
