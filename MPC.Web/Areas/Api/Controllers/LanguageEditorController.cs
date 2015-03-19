using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Resource File for Language Editor
    /// </summary>
    public class LanguageEditorController : ApiController
    {

        #region Private
        private readonly IMyOrganizationService myOrganizationService;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public LanguageEditorController(IMyOrganizationService myOrganizationService)
        {
            if (myOrganizationService == null)
            {
                throw new ArgumentNullException("myOrganizationService");
            }

            this.myOrganizationService = myOrganizationService;


        }
        #endregion
        /// <summary>
        /// Read Resource File By Global Language Id
        /// </summary>
        /// <returns></returns>
        public List<LanguageEditor> Get([FromUri]int organisationId, long lanuageId)
        {
            return myOrganizationService.ReadResourceFileByLanguageId(organisationId, lanuageId).Select(le => le.CreateFrom()).ToList();
        }
    }
}