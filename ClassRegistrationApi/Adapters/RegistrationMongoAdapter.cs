using ClassRegistrationApi.Domain;
using HypertheoryApiUtils;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace ClassRegistrationApi.Adapters;

public class RegistrationMongoAdapter
{
    private readonly IMongoCollection<Registration> _documentCollection;
    private readonly ILogger<RegistrationMongoAdapter> _logger;

    public RegistrationMongoAdapter(ILogger<RegistrationMongoAdapter> logger, IOptions<MongoConnectionOptions> options)
    {
        _logger = logger;
        var clientSettings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);
        if (options.Value.LogCommands)
        {
            clientSettings.ClusterConfigurator = db =>
            {
                db.Subscribe<CommandStartedEvent>(e =>
                {
                    _logger.LogInformation($"Running {e.CommandName} - the command looks like this {e.Command.ToJson()}");
                });
            };
        }
        var conn = new MongoClient(clientSettings);

        var db = conn.GetDatabase(options.Value.Database);

        _documentCollection = db.GetCollection<Registration>(options.Value.Collection);

    }

    public IMongoCollection<Registration> GetRegistrationCollection() => _documentCollection;
}
