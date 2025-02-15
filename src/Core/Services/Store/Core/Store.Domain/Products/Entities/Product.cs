using BuildingBlock.Domain.Entities;

namespace Store.Domain.Products.Entities
{
    public class Product : Entity
    {
        public Product(
            Guid id,
            string name,
            string description,
            double price,
            List<string> images,
            ProductSubItem flavor,
            ProductSubItem additional,
            ProductSubItem preference,
            List<Product> sideDishes,
            ServesManyPeople servesManyPeople,
            Type type
            ) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Images = images;
            Flavor = flavor;
            Additional = additional;
            Preference = preference;
            SideDishes = sideDishes;
            ServesManyPeople = servesManyPeople;
            Type = type;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public double Price { get; private set; }

        public List<string> Images { get; private set; }

        /// <summary>
        /// Sabores
        /// </summary>
        public ProductSubItem Flavor { get; private set; }

        /// <summary>
        /// Adicional
        /// </summary>
        public ProductSubItem Additional { get; private set; }

        /// <summary>
        /// Preferencias
        /// </summary>
        public ProductSubItem Preference { get; private set; }

        /// <summary>
        /// Acompanhamentos
        /// </summary>
        public List<Product> SideDishes { get; private set; }

        public ServesManyPeople ServesManyPeople { get; private set; }

        public Type Type { get; private set; }
    }
}
