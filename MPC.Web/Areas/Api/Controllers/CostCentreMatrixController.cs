using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CostCentreMatrixController : ApiController
    {
        private readonly ICostCentreMatrixServices _CostCentreMatrix;

        public CostCentreMatrixController(ICostCentreMatrixServices _CostCentreMatrix)
        {
            if (_CostCentreMatrix == null)
            {
                throw new ArgumentNullException("CostCentreMatrix");
            }
            this._CostCentreMatrix = _CostCentreMatrix;
        }
        public CostCentreMatrix Post(CostCentreMatrixRequestModel Matrix)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            if (Matrix.CostCentreMatrix.MatrixId > 0)
            {
                return _CostCentreMatrix.Update(Matrix.CostCentreMatrix.CreateFrom(), Matrix.CostCentreMatrixDetail == null ? null : Matrix.CostCentreMatrixDetail.Select(g => g.CreateFrom())).CreateFrom();
            }
            else
            {
                return _CostCentreMatrix.Add(Matrix.CostCentreMatrix.CreateFrom(), Matrix.CostCentreMatrixDetail == null ? null : Matrix.CostCentreMatrixDetail.Select(g => g.CreateFrom())).CreateFrom();
            }

        }
        public bool Delete(CostCentreMatrixDeleteRequestModel req)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return  _CostCentreMatrix.DeleteMatrixById(req.MatrixId);
            

        }
    }
}