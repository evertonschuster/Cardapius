namespace Administration.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork
        ) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<Result<CreateProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var sideDishes = productRepository.ListWithAllPropertyByIds(request.SideDishes ?? []);
            var model = request.ToModel(sideDishes);
            var result = productRepository.Create(model);

            unitOfWork.Commit();
            var responde = Result<CreateProductResult>.Success(new CreateProductResult
            {
                Id = result.Id,
            });

            return Task.FromResult(responde);
        }
    }
}
