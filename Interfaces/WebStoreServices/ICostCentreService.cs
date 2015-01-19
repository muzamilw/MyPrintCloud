using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ICostCentreService
    {
        object CompileBinaries(string sOutputPath, string Source, string CompanyName);


        void CompileCostCentreTest();

        void SaveCostCentre(long _CostCentreID, long OrganisationId, string OrganisationName);

        double ExecuteVariable(ref object[] oParamsArray, int VariableID);

        double ExecuteResource(ref object[] oParamsArray, long ResourceID, string ReturnValue);

        double ExecuteUserStockItem(int StockID, StockPriceType StockPriceType, out double Price, out double PerQtyQty);
        double ExecuteQuestion(ref object[] oParamsArray, int QuestionID, long CostCentreID);
        double ExecuteMatrix(ref object[] oParamsArray, int MatrixID, long CostCentreID);
    }
}
