using Microsoft.EntityFrameworkCore;
using PXBank.CiotProviderService.Domain.Enums;
using PXBank.CiotProviderService.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PXBank.CiotProviderService.Infra.Data.Context.Configurations.Base;
using PXBank.CiotProviderService.Infra.CrossCutting.Commons.Extensions;

namespace PXBank.CiotProviderService.Infra.Data.Context.Configurations
{
    public class VeiculoEntityTypeConfiguration : EntityTypeConfiguration<Veiculo>
    {
        public override void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.DeclaracaoOperacaoTransporteId)
              .HasColumnType("uuid");

            builder.Property(x => x.Placa)
                .HasColumnType("varchar(7)")
                .IsRequired();

            builder.Property(x => x.Rntrc)
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(x => x.CreateAt)
                .HasColumnType("timestamp without time zone");

            builder.Property(x => x.ModifyAt)
                .HasColumnType("timestamp without time zone");

        }
    }
}
