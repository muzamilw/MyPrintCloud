using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface INABTransactionService
    {
        long NabTransactionSaveRequest(int EstimateId, string Request);
        bool NabTransactionUpdateRequest(long TransdactionId, string Response);
    }
}
