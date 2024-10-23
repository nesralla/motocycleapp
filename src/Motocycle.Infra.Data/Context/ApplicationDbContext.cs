using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Newtonsoft.Json;

namespace Motocycle.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DbSettingsProvider _dbSettings;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, DbSettingsProvider dbSettings)
            : base(options)
        {
            _dbSettings = dbSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(_dbSettings.ConnectionString);

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Model.GetEntityTypes().ToList().ForEach(entityType =>
            {
                entityType.SetTableName(entityType.DisplayName());

                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);

                entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(string))
                    .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name))
                    .ToList()
                    .ForEach(property =>
                    {
                        property.IsUnicode(false);
                        property.HasMaxLength(2000);
                    });
            });
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
