namespace Administration.Application.Products.Commands.CreateProduct
{
    internal class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("A descrição não pode exceder 500 caracteres.");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("O preço é obrigatório.");

            RuleFor(x => x.Images)
                .NotEmpty()
                .WithMessage("Pelo menos uma imagem é obrigatória.");

            RuleFor(x => x.TypeId)
                .NotEmpty()
                .WithMessage("O ID do tipo de produto é obrigatório.");

            RuleForEach(x => x.SideDishes)
                .Must(id =>
                {
                    return _productRepository.ExistsById(id, CancellationToken.None);
                })
                .WithMessage("Acompanhamentos não encontrado.");

            RuleFor(x => x.TypeId)
                .Must(id =>
                {
                    return true;//TODO: Implementar a verificação de existência do tipo de produto
                })
                .WithMessage("Tipo de produto não encontrado.");

        }
    }
}
