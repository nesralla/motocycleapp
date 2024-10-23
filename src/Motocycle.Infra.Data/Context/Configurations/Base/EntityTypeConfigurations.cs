using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Motocycle.Domain.Core.Models;

namespace Motocycle.Infra.Data.Base
{
    public class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.CreateAt)
                   .HasDefaultValueSql("now()")
                   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
        }
    }
}
