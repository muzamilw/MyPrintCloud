﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.WebStoreServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
namespace MPC.Implementation.WebStoreServices
{
    public class PrePaymentService : IPrePaymentService
    {
        public readonly IPrePaymentRepository _PrePaymentRepository;
        public PrePaymentService(IPrePaymentRepository PrePaymentRepository)
        {
            this._PrePaymentRepository = PrePaymentRepository;
        }

        public void CreatePrePayment(PaymentMethods payMethod, long orderID, int? customerID, long payPalResponseID, string transactionID, double amountReceived, StoreMode Mode, string responsecodeNab = "")
        {
            _PrePaymentRepository.CreatePrePayment(payMethod, orderID, customerID, payPalResponseID, transactionID, amountReceived, Mode, responsecodeNab);

        }

    }
}
