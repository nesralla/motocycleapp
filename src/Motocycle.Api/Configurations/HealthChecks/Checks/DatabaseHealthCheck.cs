using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Motocycle.Api.Configurations.HealthChecks.Checks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        public static string Name { get { return nameof(DatabaseHealthCheck); } }
        private const string _defaultQuery = "SELECT 1";
        private readonly DbSettingsProvider _dbSettings;

        public DatabaseHealthCheck(DbSettingsProvider dbSettings)
        {
            _dbSettings = dbSettings;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var connectionString = _dbSettings.ConnectionString;
            var dataSource = Regex.Match(connectionString, @"host=([A-Za-z0-9_.]+)", RegexOptions.IgnoreCase).Value;

            using var connection = new NpgsqlConnection(connectionString);
            try
            {
                await connection.OpenAsync(cancellationToken);

                var command = connection.CreateCommand();
                command.CommandText = _defaultQuery;

                await command.ExecuteNonQueryAsync(cancellationToken);

                return HealthCheckResult.Healthy(dataSource);
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy(dataSource, e);
            }
        }
    }
}
