namespace BuildingBlock.Domain.ValueObjects.Images
{
    internal static class ImageValidator
    {
        private const int MaxAlternativeTextLength = 200;

        private const string EmptyUriError = "Image URI must be provided.";
        private const string InvalidUriError = "Image URI is not valid.";
        private const string EmptyAlternativeTextError = "Alternative text must be provided.";
        private static readonly string AlternativeTextTooLongError =
            $"Alternative text must be at most {MaxAlternativeTextLength} characters.";

        private const string EmptyWidthError = "Image width must be provided.";
        private const string InvalidWidthError = "Image width must be greater than zero.";
        private const string EmptyHeightError = "Image height must be provided.";
        private const string InvalidHeightError = "Image height must be greater than zero.";

        private const string EmptyThumbnailUriError = "Thumbnail URI must be provided.";
        private const string InvalidThumbnailUriError = "Thumbnail URI is not valid.";

        private const string EmptyBlurHashError = "Blur hash must be provided.";
        private const string InvalidBlurHashError = "Blur hash is not valid.";

        public static ValidationResult Validate(
            string? uri,
            string? alternativeText,
            int? width,
            int? height,
            string? thumbnailUri,
            string? blurHash)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return ValidationResult.Failure(EmptyUriError);

            var trimmedUri = uri.Trim();
            if (!Uri.IsWellFormedUriString(trimmedUri, UriKind.Absolute))
                return ValidationResult.Failure(InvalidUriError);

            if (string.IsNullOrWhiteSpace(alternativeText))
                return ValidationResult.Failure(EmptyAlternativeTextError);

            var trimmedAlt = alternativeText.Trim();
            if (trimmedAlt.Length > MaxAlternativeTextLength)
                return ValidationResult.Failure(AlternativeTextTooLongError);

            if (!width.HasValue)
                return ValidationResult.Failure(EmptyWidthError);

            if (width <= 0)
                return ValidationResult.Failure(InvalidWidthError);

            if (!height.HasValue)
                return ValidationResult.Failure(EmptyHeightError);

            if (height <= 0)
                return ValidationResult.Failure(InvalidHeightError);

            if (string.IsNullOrWhiteSpace(thumbnailUri))
                return ValidationResult.Failure(EmptyThumbnailUriError);

            var trimmedThumbnail = thumbnailUri.Trim();
            if (!Uri.IsWellFormedUriString(trimmedThumbnail, UriKind.Absolute))
                return ValidationResult.Failure(InvalidThumbnailUriError);

            if (string.IsNullOrWhiteSpace(blurHash))
                return ValidationResult.Failure(EmptyBlurHashError);

            var trimmedBlur = blurHash.Trim();
            if (trimmedBlur.Length < 6)
                return ValidationResult.Failure(InvalidBlurHashError);

            return ValidationResult.Success();
        }
    }
}
