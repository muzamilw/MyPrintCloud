using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MPC.Implementation.WebStoreServices
{
    public class NABTransactionService : INABTransactionService
    {
        private readonly INABTransactionRepository _INABTransactionRepository;
        public NABTransactionService(INABTransactionRepository _INABTransactionRepository)
        {
            this._INABTransactionRepository = _INABTransactionRepository;
        }


        public long NabTransactionSaveRequest(int EstimateId, string Request)
        {
            return _INABTransactionRepository.NabTransactionSaveRequest(EstimateId, Request);
        }
        public bool NabTransactionUpdateRequest(long TransdactionId, string Response)
        {
            return _INABTransactionRepository.NabTransactionUpdateRequest(TransdactionId, Response);
        }
    }
}
