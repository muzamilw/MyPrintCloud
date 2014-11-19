using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.IServices
{
    public interface IPaperSheetService
    {
        
        /// <summary>
        /// Get All Paper Sheets
        /// </summary>
        /// <returns></returns>
        IEnumerable<PaperSize> GetAll();
        
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
