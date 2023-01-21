
using System.Runtime.CompilerServices;

namespace BuildingBlock.Domain.Entities
{
    public abstract partial class Entity : IEquatable<Entity>
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity)
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            Entity item = (Entity)obj;

            return item.Id == this.Id;
        }

        public bool Equals(Entity? other)
        {
            return this.Equals(other as object);
        }

        public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
