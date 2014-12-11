using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    public class UserManagerService : IUserManagerService
    {
        public readonly IUserManagerRepository _UserRepository;

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public UserManagerService(IUserManagerRepository UserManagerRepository)
        {
            this._UserRepository = UserManagerRepository;
          
        }


        #endregion

        public SystemUser GetSalesManagerDataByID(int ManagerId)
        {
            return _UserRepository.GetSalesManagerDataByID(ManagerId);
           
        }
    }
}
