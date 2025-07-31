using Administration.Domain.Products.Entities;

namespace Administration.Application.Products.Queries.DetailsById
{
    public class DetailsByIdResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsByIdResult"/> class by mapping properties from the specified <see cref="Product"/>.
        /// </summary>
        /// <param name="product">The product entity to map from. Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="product"/> is null.</exception>
        public DetailsByIdResult(Product? product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            PreparationTime = product.PreparationTime;
            Images = product.Images;
            Flavor = product.Flavor;
            Additional = product.Additional;
            Preference = product.Preference;
            SideDishes = [.. product.SideDishes.Select(e => new DetailsByIdResult(e))];
            ServesManyPeople = product.ServesManyPeople;
        }

        public Guid Id { get; init; }
        public ProductName Name { get; init; }

        public string? Description { get; init; }

        public ProductionPrice Price { get; init; }

        public PreparationTime PreparationTime { get; init; }

        public List<Image> Images { get; init; }

        /// <summary>
        /// Sabores
        /// </summary>
        public ProductSubItem? Flavor { get; init; }

        /// <summary>
        /// Adicional
        /// </summary>
        public ProductSubItem? Additional { get; init; }

        /// <summary>
        /// Preferencias
        /// </summary>
        public ProductSubItem? Preference { get; init; }

        /// <summary>
        /// Acompanhamentos
        /// </summary>
        public List<DetailsByIdResult> SideDishes { get; init; } = [];

        public ServesManyPeople? ServesManyPeople { get; init; }

    }
}
