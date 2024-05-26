using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SMCTPortal.DataAccess.DatabaseContext;

public class MongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDBConnection");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(configuration["SMZT"]);
    }

    // Add DbSet properties for your MongoDB collections here

    // Example:
    // public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    public Citizen Citizens { get; set; }
}
