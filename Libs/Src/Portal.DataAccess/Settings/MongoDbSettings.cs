using SMCTPortal.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCTPortal.DataAccess.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DbName { get; set; }
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        
    }
}
