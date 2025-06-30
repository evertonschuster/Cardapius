namespace BuildingBlock.Domain.ValueObjects.Media
{
    internal static class ImageValidator
    {
        private const int MaxAlternativeTextLength = 200;

        private const string EmptyUriError = "A URI da imagem deve ser informada.";
        private const string InvalidUriError = "A URI da imagem não é válida.";
        private const string EmptyAlternativeTextError = "O texto alternativo deve ser informado.";
        private static readonly string AlternativeTextTooLongError =
            $"O texto alternativo deve ter no máximo {MaxAlternativeTextLength} caracteres.";

        private const string EmptyWidthError = "A largura da imagem deve ser informada.";
        private const string InvalidWidthError = "A largura da imagem deve ser maior que zero.";
        private const string EmptyHeightError = "A altura da imagem deve ser informada.";
        private const string InvalidHeightError = "A altura da imagem deve ser maior que zero.";

        private const string EmptyThumbnailUriError = "A URI da miniatura deve ser informada.";
        private const string InvalidThumbnailUriError = "A URI da miniatura não é válida.";

        private const string EmptyBlurHashError = "O blur hash deve ser informado.";
        private const string InvalidBlurHashError = "O blur hash não é válido.";

        public static ValidationResult Validate(
            string? uri,
            string? alternativeText,
            int? width,
            int? height,
            string? thumbnailUri,
            string? blurHash)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return ValidationResult.Failure(nameof(Image.Uri), EmptyUriError);

            var trimmedUri = uri.Trim();
            if (!Uri.IsWellFormedUriString(trimmedUri, UriKind.Absolute))
                return ValidationResult.Failure(nameof(Image.ThumbnailUri), InvalidUriError);

            if (string.IsNullOrWhiteSpace(alternativeText))
                return ValidationResult.Failure(nameof(Image.AlternativeText), EmptyAlternativeTextError);

            var trimmedAlt = alternativeText.Trim();
            if (trimmedAlt.Length > MaxAlternativeTextLength)
                return ValidationResult.Failure(nameof(Image.AlternativeText), AlternativeTextTooLongError);

            if (!width.HasValue)
                return ValidationResult.Failure(nameof(Image.Width), EmptyWidthError);

            if (width <= 0)
                return ValidationResult.Failure(nameof(Image.Width), InvalidWidthError);

            if (!height.HasValue)
                return ValidationResult.Failure(nameof(Image.Height), EmptyHeightError);

            if (height <= 0)
                return ValidationResult.Failure(nameof(Image.Height), InvalidHeightError);

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
