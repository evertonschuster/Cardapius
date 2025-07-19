using BuildingBlock.Domain;

namespace Administration.Application.Products.Commands.CreateProduct
{
    internal class CreateProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var sideDishes = productRepository.GetWithAllPropertyByIds(request.SideDishes ?? []);
            var model = request.ToModel(sideDishes);
            var result = productRepository.Create(model);

            unitOfWork.Commit();

            return Task.FromResult(new CreateProductResult
            {
                Id = result.Id,
            });
        }
    }
}
