using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business
{
    public class BankInformationTests
    {
        [Fact]
        public void Empty_DeveRetornarValoresPadrao()
        {
            var empty = BankInformation.Empty;
            empty.Bank.Should().Be("Banco");
            empty.Agency.Should().Be("0001");
            empty.AccountNumber.Should().Be("12345-6");
            empty.AccountType.Should().Be(AccountType.Checking);
            empty.PixKeys.Should().ContainSingle().And.Contain("pix@teste.com");
        }

        [Fact]
        public void Create_DeveRetornarSucesso_QuandoDadosValidos()
        {
            var result = BankInformation.Create(
                "Banco do Brasil",
                "1234",
                "98765-4",
                AccountType.Savings,
                new[] { "chave@pix.com" }
            );

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.Bank.Should().Be("Banco do Brasil");
            result.Value.Agency.Should().Be("1234");
            result.Value.AccountNumber.Should().Be("98765-4");
            result.Value.AccountType.Should().Be(AccountType.Savings);
            result.Value.PixKeys.Should().ContainSingle().And.Contain("chave@pix.com");
        }

        [Fact]
        public void Create_DeveRetornarFalha_QuandoDadosInvalidos()
        {
            var result = BankInformation.Create(
                null,
                "",
                null,
                AccountType.Checking,
                null
            );

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoDadosValidos()
        {
            var info = new BankInformation
            {
                Bank = "Banco Inter",
                Agency = "0002",
                AccountNumber = "54321-0",
                AccountType = AccountType.Checking,
                PixKeys = new List<string> { "inter@pix.com" }
            };

            var result = info.Validate();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_DeveRetornarFalha_QuandoDadosInvalidos()
        {
            var info = new BankInformation
            {
                Bank = "",
                Agency = "",
                AccountNumber = "",
                AccountType = AccountType.Checking,
                PixKeys = new List<string>()
            };

            var result = info.Validate();
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }
    }
}