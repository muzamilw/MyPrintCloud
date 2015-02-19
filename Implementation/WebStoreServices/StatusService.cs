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
    class StatusService : IStatusService
    {
        private IStatusRepository _statusrepository;
        public StatusService(IStatusRepository _statusrepository)
        {
            this._statusrepository = _statusrepository;
        }


        public List<Status> GetStatusListByStatusTypeID(int statusTypeID)
        {
            return _statusrepository.GetStatusListByStatusTypeID(statusTypeID);
        }
    }
}
