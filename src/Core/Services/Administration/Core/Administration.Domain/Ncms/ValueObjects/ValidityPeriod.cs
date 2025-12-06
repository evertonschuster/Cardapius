namespace Administration.Domain.Ncms.ValueObjects
{
    public record ValidityPeriod : ValueObject
    {
        public DateOnly Start { get; init; }
        public DateOnly? End { get; init; }

        public ValidityPeriod(DateOnly start, DateOnly? end)
        {
            if (end.HasValue && end.Value < start)
            {
                throw new ArgumentException("Data final não pode ser anterior à data inicial.", nameof(end));
            }

            Start = start;
            End = end;
        }
    }
}
