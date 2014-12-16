using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Webstore.Models; 

namespace MPC.Webstore.ModelMappers
{
    public static class OrganisationMapper
    {
        #region

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static ApiModels.Organisation CreateFrom(this DomainModels.Organisation source)
        {
            return new ApiModels.Organisation
            {
                OrganisationId = source.OrganisationId,
                OrganisationName = source.OrganisationName,
                SmtpServer = source.SmtpServer,
                SmtpUserName = source.SmtpUserName,
                SmtpPassword = source.SmtpPassword
                
            };
        }




        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Organisation CreateFrom(this ApiModels.Organisation source)
        {
            return new DomainModels.Organisation
            {
                OrganisationId = source.OrganisationId,
                OrganisationName = source.OrganisationName,
                SmtpServer = source.SmtpServer,
                SmtpUserName = source.SmtpUserName,
                SmtpPassword = source.SmtpPassword
            };
        }


        #endregion
    }
}