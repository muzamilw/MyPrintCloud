using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;


namespace MPC.Interfaces.Repository
{
    public interface INABTransactionRepository : IBaseRepository<NABTransaction, long>
    {

        long NabTransactionSaveRequest(int EstimateId, string Request);
    }
}
