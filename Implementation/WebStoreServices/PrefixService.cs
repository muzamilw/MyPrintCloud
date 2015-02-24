using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.WebStoreServices
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
        public Prefix GetDefaultPrefix()
        {
            return _prefixRepository.GetDefaultPrefix();
        }
    }
}
