using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface IPaperSheetService
    {
        
        /// <summary>
        /// Get All Paper Sheets
        /// </summary>
        /// <returns></returns>
        PaperSheetResponse GetAll(PaperSheetRequestModel request);
        
        /// <summary>
        /// Add New paper Sheet
        /// </summary>
        /// <param name="paperSize"></param>
        /// <returns></returns>
        PaperSize Add(PaperSize paperSize);
        
        /// <summary>
        /// Update New paper Sheet
        /// </summary>
        /// <param name="paperSize"></param>
        /// <returns></returns>
        PaperSize Update(PaperSize paperSize);

        /// <summary>
        /// Delete paper Sheet
        /// </summary>
        /// <param name="paperSizeId"></param>
        /// <returns></returns>
        bool Delete(int paperSizeId);

        /// <summary>
        /// Get Paper Sheet By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        PaperSize GetPaperSheetById(int id);
    }
}
