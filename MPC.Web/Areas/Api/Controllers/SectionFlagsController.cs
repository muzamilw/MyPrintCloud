﻿using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SectionFlag = MPC.Models.DomainModels.SectionFlag;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    ///Section Flags API Controller
    /// </summary>
    public class SectionFlagsController : ApiController
    {
        #region Private
        private readonly ISectionService sectionService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SectionFlagsController( ISectionService sectionService)
        {
            this.sectionService = sectionService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Section Flags by Section Id
        /// </summary>
        public IEnumerable<Models.SectionFlag> Get(long sectionId)
        {
           return sectionService.GetSectionFlagBySectionId(sectionId).CreateFrom();
        }

        /// <summary>
        /// Save Section Flags
        /// </summary>
        [HttpPost]
        public bool Post( SectionFlagUpdateModel flags)
        {
            if (flags != null)
            {
                sectionService.SaveSectionFlags(flags.SectionFlags.Select(flag => flag.CreateFrom()));
                return true;
            }
            return false;
        }
        #endregion
    }
}