using BuildingBlock.Domain.ValueObjects.Media;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Media
{
    public class ImageValidatorTests
    {
        private const string ValidUri = "https://example.com/image.jpg";
        private const string ValidThumbnailUri = "https://example.com/thumb.jpg";
        private const string ValidAlt = "Descrição da imagem";
        private const string ValidBlurHash = "LEHV6nWB2yk8pyo0adR*.7kCMdnj";
        private const int ValidWidth = 100;
        private const int ValidHeight = 200;

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoUriVazia(string? uri)
        {
            var result = ImageValidator.Validate(uri, ValidAlt, ValidWidth, ValidHeight, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("A URI da imagem deve ser informada.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoUriInvalida()
        {
            var result = ImageValidator.Validate("not-an-uri", ValidAlt, ValidWidth, ValidHeight, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("A URI da imagem não é válida.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoUriMuitoLonga()
        {
            var longUri = "https://example.com/" + new string('a', Image.MaxUriLength + 1);
            var result = ImageValidator.Validate(longUri, ValidAlt, ValidWidth, ValidHeight, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be($"A URI da imagem deve ter no máximo {Image.MaxUriLength} caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoAlternativeTextVazio(string? alt)
        {
            var result = ImageValidator.Validate(ValidUri, alt, ValidWidth, ValidHeight, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O texto alternativo deve ser informado.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoAlternativeTextMuitoLongo()
        {
            var longAlt = new string('a', Image.MaxAlternativeTextLength + 1);
            var result = ImageValidator.Validate(ValidUri, longAlt, ValidWidth, ValidHeight, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be($"O texto alternativo deve ter no máximo {Image.MaxAlternativeTextLength} caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoWidthNaoInformado()
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, null, ValidHeight, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("A largura da imagem deve ser informada.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_DeveRetornarErro_QuandoWidthInvalido(int width)
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, width, ValidHeight, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("A largura da imagem deve ser maior que zero.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoHeightNaoInformado()
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, null, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("A altura da imagem deve ser informada.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_DeveRetornarErro_QuandoHeightInvalido(int height)
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, height, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("A altura da imagem deve ser maior que zero.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoThumbnailUriVazia(string? thumb)
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, ValidHeight, thumb, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("A URI da miniatura deve ser informada.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoThumbnailUriInvalida()
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, ValidHeight, "not-an-uri", ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("A URI da miniatura não é válida.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoThumbnailUriMuitoLonga()
        {
            var longThumb = "https://example.com/" + new string('b', Image.MaxThumbnailUriLength + 1);
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, ValidHeight, longThumb, ValidBlurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be($"A URI da miniatura deve ter no máximo {Image.MaxThumbnailUriLength} caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoBlurHashVazio(string? blurHash)
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, ValidHeight, ValidThumbnailUri, blurHash);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O blur hash deve ser informado.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoBlurHashMuitoCurto()
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, ValidHeight, ValidThumbnailUri, "12345");
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O blur hash não é válido.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoBlurHashMuitoLongo()
        {
            var longBlur = new string('c', Image.MaxBlurHashLength + 1);
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, ValidHeight, ValidThumbnailUri, longBlur);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be($"O blur hash deve ter no máximo {Image.MaxBlurHashLength} caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoTodosCamposValidos()
        {
            var result = ImageValidator.Validate(ValidUri, ValidAlt, ValidWidth, ValidHeight, ValidThumbnailUri, ValidBlurHash);
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
