using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ICostCentreMatrixRepository
    {
        CostCentreMatrix GetMatrix(int MatrixID);

        List<CostCentreMatrix> GetMatrixByOrganisationID(long OrganisationID, out List<CostCentreMatrixDetail> matrixDetail);

        IEnumerable<CostCentreMatrixDetail> GetByMatrixId(int MatrixId);
        CostCentreMatrix Add(CostCentreMatrix Matrix, IEnumerable<CostCentreMatrixDetail> MatrixDetail);
        CostCentreMatrix Update(CostCentreMatrix Matrix, IEnumerable<CostCentreMatrixDetail> MatrixDetail);
        bool DeleteMatrixById(int MatrixId);
    }
}
