namespace BuildingBlock.Domain.ValueObjects.Media
{
    internal static class ImageValidator
    {
        private const string EmptyUriError = "A URI da imagem deve ser informada.";
        private const string InvalidUriError = "A URI da imagem não é válida.";
        private static readonly string UriTooLongError = $"A URI da imagem deve ter no máximo {Image.MaxUriLength} caracteres.";

        private const string EmptyAlternativeTextError = "O texto alternativo deve ser informado.";
        private static readonly string AlternativeTextTooLongError = $"O texto alternativo deve ter no máximo {Image.MaxAlternativeTextLength} caracteres.";

        private const string EmptyWidthError = "A largura da imagem deve ser informada.";
        private const string InvalidWidthError = "A largura da imagem deve ser maior que zero.";

        private const string EmptyHeightError = "A altura da imagem deve ser informada.";
        private const string InvalidHeightError = "A altura da imagem deve ser maior que zero.";

        private const string EmptyThumbnailUriError = "A URI da miniatura deve ser informada.";
        private const string InvalidThumbnailUriError = "A URI da miniatura não é válida.";
        private static readonly string ThumbnailUriTooLongError = $"A URI da miniatura deve ter no máximo {Image.MaxThumbnailUriLength} caracteres.";

        private const string EmptyBlurHashError = "O blur hash deve ser informado.";
        private const string InvalidBlurHashError = "O blur hash não é válido.";
        private static readonly string BlurHashTooLongError = $"O blur hash deve ter no máximo {Image.MaxBlurHashLength} caracteres.";

        /// <summary>
        /// Validates image properties including URI, alternative text, dimensions, thumbnail URI, and blur hash, returning a result indicating success or a specific validation failure.
        /// </summary>
        /// <param name="uri">The main image URI to validate.</param>
        /// <param name="alternativeText">The alternative text describing the image.</param>
        /// <param name="width">The width of the image in pixels.</param>
        /// <param name="height">The height of the image in pixels.</param>
        /// <param name="thumbnailUri">The URI of the image thumbnail.</param>
        /// <param name="blurHash">The blur hash string representing a blurred preview of the image.</param>
        /// <returns>A <see cref="Result"/> indicating validation success or failure with an appropriate error message.</returns>
        public static Result Validate(
            string? uri,
            string? alternativeText,
            int? width,
            int? height,
            string? thumbnailUri,
            string? blurHash)
        {
            // URI principal
            if (string.IsNullOrWhiteSpace(uri))
                return Result.Fail(nameof(Image.Uri), EmptyUriError);

            var trimmedUri = uri.Trim();
            if (!Uri.IsWellFormedUriString(trimmedUri, UriKind.Absolute))
                return Result.Fail(nameof(Image.Uri), InvalidUriError);

            if (trimmedUri.Length > Image.MaxUriLength)
                return Result.Fail(nameof(Image.Uri), UriTooLongError);

            // Texto alternativo
            if (string.IsNullOrWhiteSpace(alternativeText))
                return Result.Fail(nameof(Image.AlternativeText), EmptyAlternativeTextError);

            var trimmedAlt = alternativeText.Trim();
            if (trimmedAlt.Length > Image.MaxAlternativeTextLength)
                return Result.Fail(nameof(Image.AlternativeText), AlternativeTextTooLongError);

            // Width / Height
            if (!width.HasValue)
                return Result.Fail(nameof(Image.Width), EmptyWidthError);

            if (width <= 0)
                return Result.Fail(nameof(Image.Width), InvalidWidthError);

            if (!height.HasValue)
                return Result.Fail(nameof(Image.Height), EmptyHeightError);

            if (height <= 0)
                return Result.Fail(nameof(Image.Height), InvalidHeightError);

            // URI da miniatura
            if (string.IsNullOrWhiteSpace(thumbnailUri))
                return Result.Fail(nameof(Image.ThumbnailUri), EmptyThumbnailUriError);

            var trimmedThumbnail = thumbnailUri.Trim();
            if (!Uri.IsWellFormedUriString(trimmedThumbnail, UriKind.Absolute))
                return Result.Fail(nameof(Image.ThumbnailUri), InvalidThumbnailUriError);

            if (trimmedThumbnail.Length > Image.MaxThumbnailUriLength)
                return Result.Fail(nameof(Image.ThumbnailUri), ThumbnailUriTooLongError);

            // BlurHash
            if (string.IsNullOrWhiteSpace(blurHash))
                return Result.Fail(nameof(Image.BlurHash), EmptyBlurHashError);

            var trimmedBlur = blurHash.Trim();
            if (trimmedBlur.Length < 6)
                return Result.Fail(nameof(Image.BlurHash), InvalidBlurHashError);

            if (trimmedBlur.Length > Image.MaxBlurHashLength)
                return Result.Fail(nameof(Image.BlurHash), BlurHashTooLongError);

            return Result.Success();
        }
    }
}
