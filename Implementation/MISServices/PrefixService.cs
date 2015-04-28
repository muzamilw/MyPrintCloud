using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class PrefixService : IPrefixService
    {
        #region Private

        private readonly IPrefixRepository _prefixRepository;
        #endregion

        #region Constructor
        public PrefixService(IPrefixRepository prefixRepository)
        {
            this._prefixRepository = prefixRepository;
        }
        #endregion

        #region Public

        public Prefix Add(Prefix prefix)
        {
           _prefixRepository.Add(prefix);
            _prefixRepository.SaveChanges();
            return prefix;
        }

        public Prefix Update(Prefix prefix)
        {
            _prefixRepository.Update(prefix);
            _prefixRepository.SaveChanges();
            return prefix;
        }

        public bool Delete(int prefixId)
        {
            _prefixRepository.Delete(GetPrefixByOrganisationId());
            _prefixRepository.SaveChanges();
            return true;
        }

        public Prefix GetPrefixByOrganisationId()
        {
            return _prefixRepository.GetPrefixByOrganisationId(_prefixRepository.OrganisationId);
        }
        
        #endregion



    }
}
