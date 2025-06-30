using Administration.Application.Products;

namespace Administration.Domain.Products.Dtos
{
    public class CreateProductDto
    {
        public ProductName Name { get; set; }
        public string? Description { get; set; }
        public ProductionPrice Price { get; set; }
        public PreparationTime PreparationTime { get; set; }
        public List<Image> Images { get; set; } = new();
        public ProductSubItem? Flavor { get; set; }
        public ProductSubItem? Additional { get; set; }
        public ProductSubItem? Preference { get; set; }
        public List<Product>? SideDishes { get; set; }
        public ServesManyPeople? ServesManyPeople { get; set; }
        public Guid TypeId { get; set; }
    }
}
