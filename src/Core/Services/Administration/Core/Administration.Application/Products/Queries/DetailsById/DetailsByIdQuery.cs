namespace Administration.Application.Products.Queries.DetailsById
{
    public class DetailsByIdQuery : IQueryRequest<DetailsByIdResult>
    {
        public DetailsByIdQuery(Guid productId)
        {
            Id = productId;
        }

        public Guid Id { get; init; }

    }
}
