using Administration.Domain.Products.Dtos;

namespace Administration.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductResult>
    {
        public ProductName Name { get; set; }

        public string? Description { get; set; }

        public required ProductionPrice Price { get; set; }

        public PreparationTime PreparationTime { get; set; }

        public required List<Image> Images { get; set; }

        /// <summary>
        /// Sabores
        /// </summary>
        public ProductSubItem? Flavor { get; set; }

        /// <summary>
        /// Adicional
        /// </summary>
        public ProductSubItem? Additional { get; set; }

        /// <summary>
        /// Preferencias
        /// </summary>
        public ProductSubItem? Preference { get; set; }

        /// <summary>
        /// Acompanhamentos
        /// </summary>
        public List<Guid>? SideDishes { get; set; }

        public ServesManyPeople? ServesManyPeople { get; set; }

        public Guid TypeId { get; set; }

        internal Product ToModel(List<Product> sideDishes)
        {
            var dto = new CreateProductDto()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                PreparationTime = PreparationTime,
                Images = Images,
                Flavor = Flavor,
                Additional = Additional,
                Preference = Preference,
                SideDishes = sideDishes,
                ServesManyPeople = ServesManyPeople,
                TypeId = TypeId
            };

            return Product.Create(dto);
        }
    }
}
