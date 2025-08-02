namespace Administration.Application.Products.Queries.DetailsById
{
    internal class DetailsByIdHandler(IProductRepository productRepository) : IQueryHandler<DetailsByIdQuery, DetailsByIdResult>
    {
        /// <summary>
        /// Handles a query to retrieve detailed product information by ID.
        /// </summary>
        /// <param name="request">The query containing the product ID.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task containing the result with product details if found, or a failure result if the product does not exist.</returns>
        public Task<Result<DetailsByIdResult>> Handle(DetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = productRepository.GetWithAllPropertyByIds(request.Id);
            if (product is null)
            {
                return Task.FromResult(Result<DetailsByIdResult>.Fail(nameof(DetailsByIdQuery.Id), "Produto não encontrado."));
            }

            var result = Result<DetailsByIdResult>.Success(new DetailsByIdResult(product));

            return Task.FromResult(result);
        }
    }
}
