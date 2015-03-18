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
        private readonly IPrefixService _prefixService;
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor Prefix Controller
        /// </summary>
        /// <param name="prefixService"></param>
        public PrefixController(IPrefixService prefixService)
        {
            this._prefixService = prefixService;
        }
        #endregion

        #region Public
        public Prefix Get()
        {
            var p = _prefixService.GetPrefixByOrganisationId();
            return p.CreateFrom();
        }

        public Prefix Post(Prefix prefix)
        {
            if (ModelState.IsValid)
            {
                return _prefixService.Update(prefix.CreateFrom()).CreateFrom();
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }
        #endregion
    }
}