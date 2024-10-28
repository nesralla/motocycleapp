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

            builder.Property(x => x.DeliverymanId)

                .IsRequired();
            builder.Property(x => x.MotocyId)

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
            builder.Property(x => x.PreviousValue)
                .HasColumnType("decimal(10,2)");
            builder.Property(x => x.FinishValue)
                .HasColumnType("decimal(10,2)");
            builder.Property(x => x.RentDays)
                .HasColumnType("int");
            builder.Property(x => x.Status)
                .HasColumnType("int");

            builder.HasOne(x => x.Motocy)
                .WithMany(f => f.Rents)
                .HasForeignKey(x => x.MotocyId);

            builder.HasOne(x => x.Deliveryman)
                .WithMany(f => f.Rents)
                .HasForeignKey(x => x.DeliverymanId);


        }
    }
}
