
namespace Hexata.BI.Application.Entities
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }

        DateTime CreatedAt { get; }
    }

    public interface IEntity : IEntity<Guid>
    {

    }

    public class Entity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; }
    }
}
