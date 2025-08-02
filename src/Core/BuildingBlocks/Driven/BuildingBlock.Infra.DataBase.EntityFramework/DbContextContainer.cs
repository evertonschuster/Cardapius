using BuildingBlock.Infra.DataBase.EntityFramework.Interceptors;

namespace BuildingBlock.Infra.DataBase.EntityFramework
{
    public class DbContextContainer(
        EmitDomainEventInterceptor emitDomainEventInterceptor,
        SoftDeleteInterceptor softDeleteInterceptor,
        AuditingInterceptor auditingInterceptor)
    {
        public EmitDomainEventInterceptor EmitDomainEventInterceptor { get; } = emitDomainEventInterceptor;
        public SoftDeleteInterceptor SoftDeleteInterceptor { get; } = softDeleteInterceptor;
        public AuditingInterceptor AuditingInterceptor { get; } = auditingInterceptor;
    }
}
