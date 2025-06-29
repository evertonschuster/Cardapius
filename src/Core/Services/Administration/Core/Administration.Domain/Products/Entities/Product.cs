using Administration.Domain.Products.ValueObjects;
using BuildingBlock.Domain.ValueObjects.Images;
using BuildingBlock.Domain.ValueObjects.Prices;
using BuildingBlock.Domain.ValueObjects.ProductNames;
using BuildingBlock.Domain.ValueObjects.Time;

namespace Administration.Application.Products
{
    public class Product : Entity
    {
        public ProductName Name { get; private set; }

        public string Description { get; private set; }

        public ProductionPrice Price { get; private set; }

        public PreparationTime PreparationTime { get; private set; }

        public List<Image> Images { get; private set; }

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

        public Guid TypeId { get; private set; }
    }
}
