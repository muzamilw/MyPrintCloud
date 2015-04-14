using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using System.Linq;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// CostCenterTreeController
    /// </summary>
    public class CostCenterTreeController : ApiController
    {
        #region Private

        private readonly ICostCentersService _costCentersService;
        private readonly ICostCentreQuestionService _ICostCentreQuestion;
        private readonly ICostCentreMatrixServices _CostCentreMatrix;
       // private readonly ICostCentreAnswerRepository ICostCentreAnswerRepository;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CostCenterTreeController(ICostCentersService costCenterService, ICostCentreQuestionService _ICostCentreQuestion, ICostCentreMatrixServices _CostCentreMatrix)
        {
            if (costCenterService == null)
            {
                throw new ArgumentNullException("costCenterService");
            }
            if (_ICostCentreQuestion == null)
            {
                throw new ArgumentNullException("ICostCentreQuestion");
            }
            if (_CostCentreMatrix == null)
            {
                throw new ArgumentNullException("ICostCentreMatrixServices");
            }
            this._CostCentreMatrix = _CostCentreMatrix;
            this._costCentersService = costCenterService;
            this._ICostCentreQuestion = _ICostCentreQuestion;
        }

        #endregion

        #region Public
        public CostCentreQuestion Put(CostCentreQuestionRequestModel QuestionRequest)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            
            return _ICostCentreQuestion.Add(QuestionRequest.Question.CreateFrom(), QuestionRequest.Answer == null ? null : QuestionRequest.Answer.Select(g => g.CreateFrom())).CreateFrom();
        }

        public bool Post(CostCentreQuestionRequestModel QuestionRequest)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _ICostCentreQuestion.update(QuestionRequest.Question.CreateFrom(), QuestionRequest.Answer == null ? null : QuestionRequest.Answer.Select(g => g.CreateFrom()));
        }
       


        public bool Delete(CostCentreQuestionDeleteRequest Req)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            if (Req.QuestionId > 0)
            {
                return _ICostCentreQuestion.DeleteQuestionById(Req.QuestionId);
            }
            else
            {
                return _ICostCentreQuestion.DeleteMCQsQuestionAnswerById(Req.MCQsQuestionAnswerId);
            }
           
        }
        public IEnumerable<CostCentreAnswer> Get(int QuestionId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _ICostCentreQuestion.GetByQuestionId(QuestionId).Select(g=>g.CreateFrom());
        }

        public IEnumerable<CostCentreMatrixDetail> GetByMatrixId(int MatrixId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _CostCentreMatrix.GetByMatrixId(MatrixId).Select(g => g.CreateFrom());
        }
        public Models.CostCenterVariablesResponseModel GetListById(int Id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _costCentersService.GetCostCenterVariablesTree(Id).CreateFrom();
        }

        #endregion
    }
}