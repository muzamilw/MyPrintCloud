﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Models.Common;

namespace MPC.Interfaces.Repository
{
    public interface IPrePaymentRepository : IBaseRepository<PrePayment, long>
    {
        void CreatePrePayment(PaymentMethods payMethod, long orderID, int? customerID, long payPalResponseID, string transactionID, double amountReceived, StoreMode Mode, string responsecodeNab = "");
    }
}
