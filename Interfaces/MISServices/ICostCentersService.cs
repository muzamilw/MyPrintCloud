﻿using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICostCentersService
    {
        IEnumerable<CostCentre> GetAll(CostCenterRequestModel request);
        CostCentre Add(CostCentre costcentre);
        CostCentre Update(CostCentre costcentre);
        bool Delete(long costcentreId);
        CostCentre GetCostCentreById(long id);
        CostCentersResponse GetUserDefinedCostCenters(CostCenterRequestModel request);
        CostCenterBaseResponse GetBaseData();
    }
}
