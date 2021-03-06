﻿
using Corex.Model.Derived.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corex.Data.Derived.EntityFramework
{
    public abstract class BaseEntityConfiguration<TEntityModel, TKey> : BaseEntityTypeConfiguration<TEntityModel, TKey>
        where TEntityModel : class, IEntityModel<TKey>
    {
        public override void Map(EntityTypeBuilder<TEntityModel> entity)
        {
            base.Map(entity);
            entity.HasKey(p => p.Id);
            entity.Property(p => p.CreatedTime).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
