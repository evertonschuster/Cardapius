using BuildingBlock.Application.Queries;

namespace Administration.Application.Products.Queries.DetailsById
{
    public class DetailsByIdQuery : IQueryRequest<DetailsByIdResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsByIdQuery"/> class with the specified product identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to retrieve details for.</param>
        public DetailsByIdQuery(Guid productId)
        {
            Id = productId;
        }

        public Guid Id { get; init; }

    }
}
