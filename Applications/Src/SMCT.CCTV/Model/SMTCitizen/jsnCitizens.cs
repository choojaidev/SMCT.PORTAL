using Amazon.SecurityToken.Model;
using SMCTPortal.DataAccess.DatabaseContext;
using SMCTPortal.DataAccess.Repositories.Implementations;
using SMCTPortal.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace SMCTPortal.Model.SMTCitizen
{
    public class jsnCitizens  
    {
        private readonly CitizensRepository _citizensRepository;
        public jsnCitizens(CitizensRepository citizensRepository)
        {
            _citizensRepository = citizensRepository;
        }
        public List<Citizen> GetCitizens()
        {
            return _citizensRepository.GetCitizens();
        }
    }
}
