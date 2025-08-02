namespace Administration.Application.Products.Queries.DetailsById
{
    internal class DetailsByIdHandler(IProductRepository productRepository) : IQueryHandler<DetailsByIdQuery, DetailsByIdResult>
    {
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
