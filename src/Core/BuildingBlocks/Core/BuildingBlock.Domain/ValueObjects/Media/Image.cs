namespace BuildingBlock.Domain.ValueObjects.Media
{
    /// <summary>
    /// Represents an image with a URI, alternative text, dimensions, thumbnail URI, and blur hash as a Value Object.
    /// </summary>
    public readonly struct Image : IValueObject, IValidatable<Image>
    {
        public string Uri { get; init; }
        public string AlternativeText { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public string ThumbnailUri { get; init; }
        public string BlurHash { get; init; }


        /// <summary>
        /// Validates this instance (after deserialization).
        /// </summary>
        public Result<Image> Validate()
        {
            var validation = ImageValidator
                .Validate(Uri, AlternativeText, Width, Height, ThumbnailUri, BlurHash);

            return Result<Image>
                .FromValidation(validation, this);
        }
    }
}
