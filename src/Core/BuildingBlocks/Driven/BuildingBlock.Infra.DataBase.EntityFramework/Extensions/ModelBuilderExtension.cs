using BuildingBlock.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace BuildingBlock.Infra.DataBase.EntityFramework.Extensions
{
    public static class ModelBuilderExtension
    {

        private const string DeletedAtPropertyName = nameof(ISoftDelete.DeletedAt);
        private const string DeletedByPropertyName = nameof(ISoftDelete.DeletedBy);

        public static ModelBuilder AddSoftDeleteAll(this ModelBuilder modelBuilder)
        {
            var entities = modelBuilder.Model
                .GetEntityTypes();

            foreach (var entity in entities)
            {
                entity.AddSoftDelete();
            }

            return modelBuilder;
        }

        public static EntityTypeBuilder AddSoftDelete(this EntityTypeBuilder entityBuilder)
        {
            entityBuilder.Metadata.AddSoftDelete();
            
            return entityBuilder;
        }

        public static IMutableEntityType AddSoftDelete(this IMutableEntityType entityType)
        {
            var hasSoftDeleteProperty = entityType.FindProperty(DeletedAtPropertyName) is not null;

            if ((!hasSoftDeleteProperty && !entityType.IsOwned()) || IsOwnedWithTable(entityType))
            {
                entityType.AddProperty(DeletedAtPropertyName, typeof(DateTimeOffset?)).SetDefaultValue(null);
                entityType.AddProperty(DeletedByPropertyName, typeof(Guid?)).SetDefaultValue(null);

                if (!entityType.IsOwned())
                {
                    entityType.SetQueryFilter(BuildIsNotDeletedFilter(entityType.ClrType));
                }
            }

            if (hasSoftDeleteProperty)
            {
                entityType.SetQueryFilter(BuildIsNotDeletedFilter(entityType.ClrType));
            }

            return entityType;
        }

        private static bool IsOwnedWithTable(IMutableEntityType entity)
        {
            var ownership = entity.FindOwnership();

            if (ownership is null)
            {
                return false;
            }

            var principalTable = ownership.PrincipalEntityType.GetTableName();
            var ownedTable = entity.GetTableName();

            return !string.Equals(principalTable, ownedTable, StringComparison.Ordinal);
        }


        private static LambdaExpression BuildIsNotDeletedFilter(Type clrType)
        {
            var param = Expression.Parameter(clrType, "e");

            var efProp = Expression.Call(
                typeof(EF), nameof(EF.Property),
                [typeof(DateTimeOffset?)],
                param,
                Expression.Constant(DeletedAtPropertyName)
            );

            var body = Expression.Equal(
                efProp,
                Expression.Constant(null, typeof(DateTimeOffset?))
            );

            return Expression.Lambda(body, param);
        }
    }
}
