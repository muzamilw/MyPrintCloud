using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ICostCentreVariableRepository
    {
        CostCentreVariable LoadVariable(int VariableId);
        double execSystemVariable(CostCentreVariable Variable, long EstimateID);
        double execSimpleVariable(CostCentreVariable Variable);
        double ExecUserVariable(CostCentreVariable oVariable);
        bool VariableExists(int VariableID, string VariableName);
        List<CostCentreVariable> returnLoadVariableList();
        IEnumerable<CostCentreVariable> GetVariableList();
    }
}
