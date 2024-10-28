using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motocycle.Domain.Enums;
using Motocycle.Domain.Models;
using Motocycle.Infra.Data.Base;

namespace Motocycle.Infra.Data.Context.Configurations
{
    public class PlanEntityTypeConfiguration : EntityTypeConfiguration<Plan>
    {
        public override void Configure(EntityTypeBuilder<Plan> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CostPerDay)
                .IsRequired();
            builder.Property(x => x.DurationDays)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnType("varchar(15)")
                .HasConversion(new EnumToStringConverter<RentPlans>());

        }
    }
}
