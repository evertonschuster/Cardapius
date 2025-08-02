namespace Administration.Domain.Products.Entities
{
    public class Product : Entity
    {
        public ProductName Name { get; init; }

        public string? Description { get; init; }

        public required ProductionPrice Price { get; init; }

        public PreparationTime PreparationTime { get; init; }

        public required List<Image> Images { get; init; }

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
        public List<Product> SideDishes { get; init; } = [];

        public ServesManyPeople? ServesManyPeople { get; init; }

        public Guid TypeId { get; init; }

        public static Product Create(CreateProductDto productDto)
        {
            var model = new Product
            {
                Id = Guid.CreateVersion7(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                PreparationTime = productDto.PreparationTime,
                Images = productDto.Images,
                Flavor = productDto.Flavor,
                Additional = productDto.Additional,
                Preference = productDto.Preference,
                SideDishes = productDto.SideDishes ?? [],
                ServesManyPeople = productDto.ServesManyPeople,
                TypeId = productDto.TypeId
            };

            model.AddDomainEvent(new ProductCreatedEvent<CreateProductDto>(model.Id, null, productDto));
            return model;
        }
    }
}
