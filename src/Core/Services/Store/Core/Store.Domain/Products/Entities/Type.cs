﻿using BuildingBlock.Domain.Entities;

namespace Store.Domain.Products.Entities
{
    public class Type : Entity
    {
        public Type(Guid id, string name, string? description) : base(id)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }
    }
}
