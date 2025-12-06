namespace Administration.Domain.Ncms.ValueObjects
{
    public record TableVersion : ValueObject
    {
        public required string Version { get; init; }
        public string? FederativeUnit { get; init; }

        public TableVersion(string version, string? federativeUnit)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentException("Versão da tabela é obrigatória.", nameof(version));
            }

            Version = version;
            FederativeUnit = string.IsNullOrWhiteSpace(federativeUnit) ? null : federativeUnit;
        }
    }
}
