namespace BuildingBlock.Domain.ValueObjects.Images
{
    /// <summary>
    /// Represents an image with a URI, alternative text, dimensions, thumbnail URI, and blur hash as a Value Object.
    /// </summary>
    public readonly struct Image : IValueObject, IValidatable<Image>
    {
        public string Uri { get; }
        public string AlternativeText { get; }
        public int Width { get; }
        public int Height { get; }
        public string ThumbnailUri { get; }
        public string BlurHash { get; }


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
