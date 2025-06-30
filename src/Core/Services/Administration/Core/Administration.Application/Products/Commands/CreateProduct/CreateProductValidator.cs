namespace Administration.Application.Products.Commands.CreateProduct
{
    internal class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O nome do produto é obrigatório.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("A descrição não pode exceder 500 caracteres.");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("O preço é obrigatório.");

            RuleFor(x => x.PreparationTime)
                .NotNull()
                .WithMessage("O tempo de preparo é obrigatório.");

            RuleFor(x => x.Images)
                .NotEmpty()
                .WithMessage("Pelo menos uma imagem é obrigatória.");

            RuleFor(x => x.TypeId)
                .NotEmpty()
                .WithMessage("O ID do tipo de produto é obrigatório.");
        }
    }
}
