using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motocycle.Domain.Models;
using Motocycle.Infra.Data.Base;

namespace Motocycle.Infra.Data.Context.Configurations
{
    public class RentEntityTypeConfiguration : EntityTypeConfiguration<Rent>
    {
        public override void Configure(EntityTypeBuilder<Rent> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Id)
                .HasColumnType("uuid");
            builder.Property(x => x.DeliverymanId)
                .HasColumnType("uuid")
                .IsRequired();
            builder.Property(x => x.MotocyId)
                .HasColumnType("uuid")
                .IsRequired();
            builder.Property(x => x.StartDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(x => x.EndDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(x => x.PreviousEndDate)
                .HasColumnType("date")
                .IsRequired();
        }
    }
}
