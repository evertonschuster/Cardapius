namespace BuildingBlock.Domain.ValueObjects.Media
{
    /// <summary>
    /// Represents an image with a URI, alternative text, dimensions, thumbnail URI, and blur hash as a Value Object.
    /// </summary>
    public class Image : IValueObject, IValidatable
    {
        public const int MaxUriLength = 256;
        public const int MaxAlternativeTextLength = 200;
        public const int MaxThumbnailUriLength = 256;
        public const int MaxBlurHashLength = 1024;

        public required string Uri { get; init; }
        public required string AlternativeText { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public required string ThumbnailUri { get; init; }
        public required string BlurHash { get; init; }


        /// <summary>
        /// Validates this instance (after deserialization).
        /// <summary>
        /// Validates the image properties and returns the result of the validation.
        /// </summary>
        /// <returns>A <see cref="Result"/> indicating whether the image properties are valid.</returns>
        public Result Validate()
        {
            return ImageValidator.Validate(Uri, AlternativeText, Width, Height, ThumbnailUri, BlurHash);
        }
    }
}
