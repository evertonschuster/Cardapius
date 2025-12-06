namespace Administration.Domain.Ncms.ValueObjects
{
    public record BenefitCode : ValueObject
    {
        public string Code { get; init; }

        public BenefitCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Código de benefício é obrigatório.", nameof(code));
            }

            Code = code;
        }

        public override string ToString() => Code;
    }
}
