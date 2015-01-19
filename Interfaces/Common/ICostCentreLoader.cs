using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using MPC.Models.Common;

namespace MPC.Interfaces.Common
{
    public interface ICostCentreLoader
    {
        object Invoke(string lcMethod, object[] Parameters);
        object test(ref object[] ParamsArray);
        CostCentreCostResult returnCost(ref object[] ParamsArray);
        CostCentrePriceResult returnPrice(ref object[] ParamsArray);
        CostCentreActualCostResult returnActualCost(ref object[] ParamsArray);
    }

}