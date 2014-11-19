using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MPC.Interfaces.IServices;
using MPC.Models.DomainModels;
using MPC.Web.ModelMappers;
using MPC.Web.Models;

namespace MPC.Web.Areas.Api.Controllers
{
    public class PaperSheetController : Controller
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