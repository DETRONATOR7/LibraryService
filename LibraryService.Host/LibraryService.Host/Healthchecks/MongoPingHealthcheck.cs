using LibraryService.Models.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryService.Host.Healthchecks
{
    public class MongoPingHealthcheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;

        public MongoPingHealthcheck(IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
                var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

                database.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}", cancellationToken: cancellationToken).Wait(cancellationToken);

                return Task.FromResult(HealthCheckResult.Healthy("MongoDb is healthy."));
            }
            catch (Exception)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "MongoDb is unhealthy."));
            }
        }
    }
}
