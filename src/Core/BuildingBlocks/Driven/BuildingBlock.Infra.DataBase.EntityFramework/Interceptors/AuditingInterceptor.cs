using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlock.Infra.DataBase.EntityFramework.Interceptors
{
    public class AuditingInterceptor : SaveChangesInterceptor
    {
    }
}
