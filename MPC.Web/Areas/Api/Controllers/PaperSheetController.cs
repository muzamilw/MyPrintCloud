﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.ModelMappers;
using MPC.Web.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class PaperSheetController : ApiController
    {
        private readonly IPaperSheetService paperSheetService;

        public PaperSheetController(IPaperSheetService paperSheetService)
        {
            this.paperSheetService = paperSheetService;
        }
        /// <summary>
        /// Get All Paper Sheets
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PaperSheet> Get()
        {
            return paperSheetService.GetAll().Select(x => x.CreateFrom());
        }
        /// <summary>
        /// Update Paper Sheet
        /// </summary>
        /// <param name="paperSheet"></param>
        /// <returns></returns>
        public PaperSheet Put(PaperSheet paperSheet)
        {
            return paperSheetService.Add(paperSheet.CreateFrom()).CreateFrom();
        }
        /// <summary>
        /// Create New Paper Sheet 
        /// </summary>
        /// <param name="paperSheet"></param>
        /// <returns></returns>
        public PaperSheet Post(PaperSheet paperSheet)
        {
            return paperSheetService.Update(paperSheet.CreateFrom()).CreateFrom();
        }
        /// <summary>
        /// Delete Paper Sheet
        /// </summary>
        /// <param name="paperSheetId"></param>
        /// <returns></returns>
        public bool Delete(int paperSheetId)
        {
            return paperSheetService.Delete(paperSheetId);
        }
    }
}