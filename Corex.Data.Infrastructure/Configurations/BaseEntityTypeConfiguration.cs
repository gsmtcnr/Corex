using Corex.Model.Derived.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corex.Data.Infrastructure
{
    public abstract class BaseEntityTypeConfiguration<TEntityModel,TKey> : IEntityTypeConfiguration
         where TEntityModel : class, IEntityModel<TKey>
    {
        public virtual void Map(EntityTypeBuilder<TEntityModel> entity)
        {
            entity.HasKey(p => p.Id);
            entity.ToTable(GetTableName(), GetSchemaName());
        }
        public virtual string GetTableName()
        {
            return typeof(TEntityModel).Name;
        }
        public virtual string GetSchemaName()
        {
            return string.Empty;
        }
    }
}
