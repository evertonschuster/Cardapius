namespace BuildingBlock.Domain.Entities
{
    public interface ISoftDelete
    {
        public DateTimeOffset? DeletedAt { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
