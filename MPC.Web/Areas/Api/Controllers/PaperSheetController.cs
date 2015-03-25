using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class PaperSheetController : ApiController
    {
        #region Private

        private readonly IPaperSheetService paperSheetService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paperSheetService"></param>
        public PaperSheetController(IPaperSheetService paperSheetService)
        {
            this.paperSheetService = paperSheetService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Paper Sheets
        /// </summary>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewPaperSheet })]
        public PaperSheetResponseModel Get([FromUri] PaperSheetRequestModel request)
        {
            PaperSheetResponse result = paperSheetService.GetAll(request);
            var paperSheets = result.PaperSizes as IList<PaperSize> ?? result.PaperSizes.ToList();
            return new PaperSheetResponseModel
                   {
                       PaperSheets = paperSheets,
                       RowCount = result.RowCount
                   };
        }

        /// <summary>
        /// Create New Paper Sheet 
        /// </summary>
        /// <param name="paperSheet"></param>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewPaperSheet })]
        public PaperSheet Post(PaperSheet paperSheet)
        {
            if (ModelState.IsValid)
            {
                // Adds new one
                if (paperSheet.PaperSizeId == 0)
                {
                    return paperSheetService.Add(paperSheet.CreateFrom()).CreateFrom();
                }
                // Updates 
                return paperSheetService.Update(paperSheet.CreateFrom()).CreateFrom();
            }
            throw new HttpException((int) HttpStatusCode.BadRequest, "Invalid Request");
        }

        /// <summary>
        /// Delete Paper Sheet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewPaperSheet })]
        public bool Delete(PaperSheetRequestModel model)
        {
            if (ModelState.IsValid)
            {
                return paperSheetService.Delete(model.PaperSheetId);
            }
            throw new HttpException((int) HttpStatusCode.BadRequest, "Invalid Request");
        }

        #endregion

    }
}