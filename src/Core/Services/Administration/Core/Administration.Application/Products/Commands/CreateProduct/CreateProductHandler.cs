namespace Administration.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork
        ) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        /// <summary>
        /// Handles the creation of a new product, including associated side dishes, and returns the result containing the new product's ID.
        /// </summary>
        /// <param name="request">The command containing product details and optional side dish IDs.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task containing the result with the ID of the newly created product.</returns>
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
