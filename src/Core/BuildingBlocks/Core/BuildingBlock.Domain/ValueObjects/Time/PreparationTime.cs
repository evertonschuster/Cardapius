namespace BuildingBlock.Domain.ValueObjects.Time
{
    /// <summary>
    /// Represents the preparation time.
    /// </summary>
    public readonly struct PreparationTime : IValueObject<TimeSpan?, PreparationTime>
    {
        public TimeSpan Value { get; }
        public static string Empty { get => "02:30:15"; }

        private PreparationTime(TimeSpan value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a validated instance of PreparationTime.
        /// </summary>
        /// <param name="value">The raw preparation duration.</param>
        /// <returns>An immutable, validated PreparationTime.</returns>
        /// <exception cref="PreparationTimeEmptyException"></exception>
        /// <exception cref="PreparationTimeNegativeException"></exception>
        public static Result<PreparationTime> Parse(TimeSpan? value)
        {
            var validation = PreparationTimeValidator.Validate(value);
            return Result<PreparationTime>.FromValidation(validation, () => new PreparationTime(value!.Value));
        }

        public override string ToString()
        {
            return $"{(int)Value.TotalHours:D2}:{Value.Minutes:D2}";
        }
    }
}
