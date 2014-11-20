using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using MPC.Interfaces.IServices;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Web.ModelMappers;
using MPC.Web.Models;

namespace MPC.Web.Areas.Api.Controllers
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
        public PaperSheetResponseModel Get()
        {
            IEnumerable<PaperSize> paperSizes = null;
            paperSizes = paperSheetService.GetAll();
            var paperSheets = paperSizes as IList<PaperSize> ?? paperSizes.ToList();
            return new PaperSheetResponseModel
                   {
                       PaperSheets = paperSheets,
                       RowCount = paperSheets.Count()
                   };
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
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(PaperSheetRequestModel model)
        {
            return paperSheetService.Delete(model.PaperSheetId);
        }
    }
}