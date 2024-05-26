using SMCTPortal.DataAccess.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCTPortal.DataAccess.Repositories.Interfaces
{
    public interface ICitizensRepository
    {
        List<Citizen> GetCitizens();
        //public string id { get; set; }
        //public string name { get; set; }
        //public string surename { get; set; }
    }
}
