using Amazon.Runtime.Internal.Settings;
using MongoDB.Driver;
using SMCTPortal.DataAccess.DatabaseContext;
using SMCTPortal.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace SMCTPortal.DataAccess.Repositories.Implementations
{
    public  class CitizensRepository : ICitizensRepository
    {
        private static List<Citizen> _Citizens = new List<Citizen>();
        private IMongoCollection<Citizen> _Collection; 
        public CitizensRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DbName);

            _Collection = database.GetCollection<Citizen>(settings.CollectionName);
        }
        public Citizen CreateCitizen(Citizen obj)
        {
            obj.id = Guid.NewGuid().ToString();
            _Citizens.Add(obj);
            return obj;
        }
        public List<Citizen> GetCitizens() {
            return _Collection.Find(_ => true).ToList();
        }
    }
}
