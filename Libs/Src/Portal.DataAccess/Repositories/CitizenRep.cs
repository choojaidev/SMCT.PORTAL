using MongoDB.Driver;
using SMCTPortal.DataAccess.DatabaseContext;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CitizenRep{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Citizen> _collection;

    public CitizenRep(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        _database = _mongoClient.GetDatabase("SMZT");
        _collection = _database.GetCollection<Citizen>("Citizens");
    }

    public async Task<IEnumerable<Citizen>> GetAllData()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}
