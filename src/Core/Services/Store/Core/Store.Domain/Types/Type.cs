using BuildingBlock.Domain.Entities;

namespace Store.Domain.Types;

public class Type : Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Type"/> class with the specified identifier, name, and optional description.
    /// </summary>
    /// <param name="id">The unique identifier for the type.</param>
    /// <param name="name">The name of the type.</param>
    /// <param name="description">An optional description of the type.</param>
    public Type(Guid id, string name, string? description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }
}
