using BuildingBlock.Infra.DataBase.EntityFramework.Interceptors;

namespace BuildingBlock.Infra.DataBase.EntityFramework
{
    public class DbContextContainer(
        SoftDeleteInterceptor softDeleteInterceptor,
        AuditingInterceptor auditingInterceptor)
    {
        public SoftDeleteInterceptor SoftDeleteInterceptor { get; } = softDeleteInterceptor;
        public AuditingInterceptor AuditingInterceptor { get; } = auditingInterceptor;
    }
}