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

        /// <summary>
        /// Adds soft delete properties and global query filters to all entity types in the model, enabling soft delete functionality across the entire model.
        /// </summary>
        /// <returns>The modified <see cref="ModelBuilder"/> with soft delete configuration applied to all entities.</returns>
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

        /// <summary>
        /// Adds soft delete properties and a global query filter to the entity type represented by the given <see cref="EntityTypeBuilder"/>.
        /// </summary>
        /// <returns>The same <see cref="EntityTypeBuilder"/> instance for chaining.</returns>
        public static EntityTypeBuilder AddSoftDelete(this EntityTypeBuilder entityBuilder)
        {
            entityBuilder.Metadata.AddSoftDelete();
            
            return entityBuilder;
        }

        /// <summary>
        /// Adds soft delete support to the specified entity type by introducing nullable <c>DeletedAt</c> and <c>DeletedBy</c> properties if they do not exist, and sets a global query filter to exclude soft deleted entities.
        /// </summary>
        /// <param name="entityType">The entity type to configure for soft delete.</param>
        /// <returns>The configured <see cref="IMutableEntityType"/> with soft delete properties and query filter applied.</returns>
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

        /// <summary>
        /// Determines whether an owned entity is mapped to a separate table from its principal entity.
        /// </summary>
        /// <param name="entity">The owned entity type to check.</param>
        /// <returns>True if the owned entity has its own table; otherwise, false.</returns>
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


        /// <summary>
        /// Builds a lambda expression that filters entities where the "DeletedAt" property is null, indicating they are not soft deleted.
        /// </summary>
        /// <param name="clrType">The CLR type of the entity.</param>
        /// <returns>A lambda expression representing the filter for non-deleted entities.</returns>
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
