using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
   public interface IStatusRepository:IBaseRepository<Status,long>
   {
       List<Status> GetStatusListByStatusTypeID(int statusTypeID);
       List<Status> GetStatusListByStatusID(int statusID);
   }

}
