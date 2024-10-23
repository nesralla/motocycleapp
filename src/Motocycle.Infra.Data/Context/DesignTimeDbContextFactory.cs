using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Motocycle.Infra.CrossCutting.Commons.Providers;

namespace Motocycle.Infra.Data.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetSection("DbSettings:ConnectionString").Value;

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var dbSettings = new DbSettingsProvider();

            optionsBuilder.UseNpgsql(connectionString, options =>
            {

                options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });

            return new ApplicationDbContext(optionsBuilder.Options, dbSettings);
        }
    }
}
