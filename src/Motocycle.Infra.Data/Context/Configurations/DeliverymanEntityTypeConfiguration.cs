using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motocycle.Domain.Models;
using Motocycle.Infra.Data.Base;

namespace Motocycle.Infra.Data.Context.Configurations
{
    public class DeliverymanEntityTypeConfiguration : EntityTypeConfiguration<Deliveryman>
    {
        public override void Configure(EntityTypeBuilder<Deliveryman> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Id)
                .HasColumnType("uuid");
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(x => x.NationalID)
                .HasColumnType("varchar(14)");
            builder.Property(x => x.DateBorn)
                .HasColumnType("date");
            builder.Property(x => x.DriveLicense)
                .HasColumnType("varchar(11)");
            builder.Property(x => x.LicenseType)
                .HasColumnType("int");
            builder.Property(x => x.DriveLicenseFile)
                .HasColumnType("varchar(500)");
        }
    }
}
