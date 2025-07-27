namespace BuildingBlock.Application
{
    public interface IUserContext
    {
        Guid UserId { get; }
        Guid TenantId { get; }
    }

    public class UserContext : IUserContext
    {
        public Guid UserId { get; private set; } = Guid.Empty;
        public Guid TenantId { get; private set; } = Guid.Empty;
    }
}
