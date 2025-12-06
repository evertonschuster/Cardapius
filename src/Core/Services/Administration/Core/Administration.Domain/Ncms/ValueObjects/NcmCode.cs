namespace Administration.Domain.Ncms.ValueObjects
{
    public record NcmCode : ValueObject
    {
        public required string Code { get; init; }

        public NcmCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Código NCM não pode ser vazio.", nameof(code));
            }

            Code = code;
        }

        public override string ToString() => Code;
    }
}
