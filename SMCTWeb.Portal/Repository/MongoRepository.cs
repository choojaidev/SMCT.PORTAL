namespace SMCTPortal.Repository
{
    using MongoDB.Driver;
    using SMCTPortal.Model.SMTCitizen;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MongoRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<modelCitizen> _collection;

        public MongoRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase("SMZT");
            _collection = _database.GetCollection<modelCitizen>("Citizens");
        }

        public async Task<IEnumerable<modelCitizen>> GetAllData()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }

}
