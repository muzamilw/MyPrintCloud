﻿using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface IDeliveryCarriersService
    {
        DeliveryCarrier Add(DeliveryCarrier adddeliverycarriers);

        DeliveryCarrier Update(DeliveryCarrier upddeliverycarriers);

        IEnumerable<DeliveryCarrier> GetAllDeliveryCarrier();

    }
}