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
       // private readonly ICostCentreAnswerRepository ICostCentreAnswerRepository;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CostCenterTreeController(ICostCentersService costCenterService, ICostCentreQuestionService _ICostCentreQuestion)
        {
            if (costCenterService == null)
            {
                throw new ArgumentNullException("costCenterService");
            }
            if (_ICostCentreQuestion == null)
            {
                throw new ArgumentNullException("ICostCentreQuestion");
            }

            this._costCentersService = costCenterService;
            this._ICostCentreQuestion = _ICostCentreQuestion;
        }

        #endregion

        #region Public


        public bool Post(CostCentreQuestionRequestModel QuestionRequest)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _ICostCentreQuestion.update(QuestionRequest.Question.CreateFrom(), QuestionRequest.Answer == null ? null : QuestionRequest.Answer.Select(g=>g.CreateFrom()));
        }

        public IEnumerable<CostCentreAnswer> Get(int QuestionId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            //return _costCentersService.GetCostCenterVariablesTree(Id).CreateFrom();
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