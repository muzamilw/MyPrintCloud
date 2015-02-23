using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class SmartFormService : ISmartFormService
    {
        public readonly ISmartFormRepository _smartFormRepository;
         #region constructor
        public SmartFormService(ISmartFormRepository smartFormRepository)
        {
            this._smartFormRepository = smartFormRepository;
 
        }
        #endregion
    }
}
