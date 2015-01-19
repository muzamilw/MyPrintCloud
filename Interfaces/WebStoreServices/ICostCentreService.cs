﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ICostCentreService
    {
        object CompileBinaries(string sOutputPath, string Source, string CompanyName);

        List<CostCentre> GetDeliveryCostCentersList();

        List<CostCentre> GetCorporateDeliveryCostCentersList(long CompanyID);
    }
}
