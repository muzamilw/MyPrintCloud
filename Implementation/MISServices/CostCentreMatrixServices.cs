using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.MISServices
{
    public class CostCentreMatrixServices : ICostCentreMatrixServices
    {
        private readonly ICostCentreMatrixRepository _CostCentreMatrix;

        public CostCentreMatrixServices(ICostCentreMatrixRepository _CostCentreMatrix)
        {
            if (_CostCentreMatrix == null)
            {
                throw new ArgumentNullException("CostCentreMatrix");
            }
            this._CostCentreMatrix = _CostCentreMatrix;
        }

        public IEnumerable<CostCentreMatrixDetail> GetByMatrixId(int MatrixId)
        {
            return _CostCentreMatrix.GetByMatrixId(MatrixId);
        }
        public CostCentreMatrix Add(CostCentreMatrix Matrix, IEnumerable<CostCentreMatrixDetail> MatrixDetail)
        {
            return _CostCentreMatrix.Add(Matrix, MatrixDetail);
        }
        public CostCentreMatrix Update(CostCentreMatrix Matrix, IEnumerable<CostCentreMatrixDetail> MatrixDetail)
        {
            return _CostCentreMatrix.Update(Matrix, MatrixDetail);
        }
        public bool DeleteMatrixById(int MatrixId)
        {
            return _CostCentreMatrix.DeleteMatrixById(MatrixId);
        }
    }
}
