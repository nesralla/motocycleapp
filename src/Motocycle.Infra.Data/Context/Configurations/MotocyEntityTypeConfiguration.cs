using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motocycle.Domain.Models;
using Motocycle.Infra.Data.Base;

namespace Motocycle.Infra.Data.Context.Configurations
{
    public class MotocyEntityTypeConfiguration : EntityTypeConfiguration<Motocy>
    {
        public override void Configure(EntityTypeBuilder<Motocy> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Id)
                .HasColumnType("uuid");
            builder.Property(x => x.Identification)
                .HasColumnType("uuid");
            builder.Property(x => x.Year)
                .HasColumnType("int");
            builder.Property(x => x.MotocyModel)
                .HasColumnType("varchar(100)");
            builder.Property(x => x.LicensePlate)
                .HasColumnType("varchar(10)")
                .IsRequired();
        }
    }
}
