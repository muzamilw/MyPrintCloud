using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class PrefixController : ApiController
    {
        #region Private
        private readonly IPrefixService _prefixServiceService;
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor Prefix Controller
        /// </summary>
        /// <param name="prefixServiceService"></param>
        public PrefixController(IPrefixService prefixServiceService)
        {
            this._prefixServiceService = prefixServiceService;
        }
        #endregion

        #region Public
        public Prefix Get()
        {
           return _prefixServiceService.GetPrefixByOrganisationId().CreateFrom();
        }

        public Prefix Post(Prefix prefix)
        {
            if (ModelState.IsValid)
            {
                return _prefixServiceService.Update(prefix.CreateFrom()).CreateFrom();
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }
        #endregion
    }
}