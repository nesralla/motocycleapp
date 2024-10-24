using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motocycle.Domain.Models;
using Motocycle.Infra.Data.Base;

namespace Motocycle.Infra.Data.Context.Configurations
{
    public class ApiErrorLogEntityTypeConfiguration : EntityTypeConfiguration<ApiErrorLog>
    {
        public override void Configure(EntityTypeBuilder<ApiErrorLog> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.RootCause)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Message)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(x => x.Type)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.ExceptionStackTrace)
                .HasColumnType("text")
                .IsRequired(false);
        }
    }
}
