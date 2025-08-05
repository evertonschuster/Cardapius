using Administration.Application.Products.Commands.CreateProduct;
using BuildingBlock.Domain.ValueObjects.Media;
using BuildingBlock.Domain.ValueObjects.Products;
using BuildingBlock.Domain.ValueObjects.Time;
using FluentValidation.TestHelper;

namespace Administration.Application.UnitTest.Products.Commands.CreateProduct
{
    public class CreateProductValidatorTests
    {
        private readonly IProductRepository _productRepository;
        private readonly CreateProductValidator _validator;

        public CreateProductValidatorTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _validator = new CreateProductValidator(_productRepository);

            _productRepository.ExistsById(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(true);
        }

        private CreateProductCommand CreateValidCommand()
        {
            return new CreateProductCommand
            {
                Name = ProductName.Parse("Produto Teste").Value,
                Description = "Descrição válida",
                Price = new ProductionPrice(), // Supondo construtor padrão válido
                PreparationTime = PreparationTime.Parse(TimeSpan.FromMinutes(10)).Value,
                Images = new List<Image> { Substitute.For<Image>() },
                Flavor = null,
                Additional = null,
                Preference = null,
                SideDishes = new List<Guid> { Guid.NewGuid() },
                ServesManyPeople = null,
                TypeId = Guid.NewGuid()
            };
        }

        [Fact]
        public void Should_Have_Error_When_Description_Too_Long()
        {
            var command = CreateValidCommand();
            command.Description = new string('a', 501);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("A descrição não pode exceder 500 caracteres.");
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Null()
        {
            var command = CreateValidCommand();
            command.Price = null!;

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Price)
                .WithErrorMessage("O preço é obrigatório.");
        }

        [Fact]
        public void Should_Have_Error_When_Images_Is_Empty()
        {
            var command = CreateValidCommand();
            command.Images = new List<Image>();

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Images)
                .WithErrorMessage("Pelo menos uma imagem é obrigatória.");
        }

        [Fact]
        public void Should_Have_Error_When_TypeId_Is_Empty()
        {
            var command = CreateValidCommand();
            command.TypeId = Guid.Empty;

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.TypeId)
                .WithErrorMessage("O ID do tipo de produto é obrigatório.");
        }

        [Fact]
        public void Should_Have_Error_When_SideDish_Not_Exists()
        {
            var sideDishId = Guid.NewGuid();
            _productRepository.ExistsById(sideDishId, Arg.Any<CancellationToken>()).Returns(false);

            var command = CreateValidCommand();
            command.SideDishes = new List<Guid> { sideDishId };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor("SideDishes[0]")
                .WithErrorMessage("Acompanhamentos não encontrado.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var sideDishId = Guid.NewGuid();
            _productRepository.ExistsById(sideDishId, Arg.Any<CancellationToken>()).Returns(true);

            var command = CreateValidCommand();
            command.SideDishes = new List<Guid> { sideDishId };

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeTrue();
        }
    }
}
