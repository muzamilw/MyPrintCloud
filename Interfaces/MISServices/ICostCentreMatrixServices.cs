using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface ICostCentreMatrixServices
    {
        IEnumerable<CostCentreMatrixDetail> GetByMatrixId(int MatrixId);
        CostCentreMatrix Add(CostCentreMatrix Matrix, IEnumerable<CostCentreMatrixDetail> MatrixDetail);
        CostCentreMatrix Update(CostCentreMatrix Matrix, IEnumerable<CostCentreMatrixDetail> MatrixDetail);
        bool DeleteMatrixById(int MatrixId);
    }
}
