using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;

namespace MPC.Implementation.MISServices
{
    public class PaperSheetService : IPaperSheetService
    {
        #region Private

        private readonly IPaperSheetRepository paperSheetRepository;

        #endregion

        #region Constructor

        public PaperSheetService(IPaperSheetRepository paperSheetRepository)
        {
            this.paperSheetRepository = paperSheetRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Paper Sheets as requested
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<PaperSize> GetAll(PaperSheetRequestModel request)
        {
            int rowCount;
            return paperSheetRepository.SearchPaperSheet(request, out rowCount);
        }
        /// <summary>
        /// Add Paper Sheet
        /// </summary>
        /// <param name="paperSize"></param>
        /// <returns></returns>
        public PaperSize Add(PaperSize paperSize)
        {
            paperSheetRepository.Add(paperSize);
            paperSheetRepository.SaveChanges();
            return paperSize;
        }
        /// <summary>
        /// Update Paper Sheet
        /// </summary>
        /// <param name="paperSize"></param>
        /// <returns></returns>
        public PaperSize Update(PaperSize paperSize)
        {
            paperSheetRepository.Update(paperSize);
            paperSheetRepository.SaveChanges();
            return paperSize;
        }
        /// <summary>
        /// Delete Paper Sheet
        /// </summary>
        /// <param name="paperSheetId"></param>
        /// <returns></returns>
        public bool Delete(int paperSheetId)
        {

            paperSheetRepository.Delete(GetPaperSheetById(paperSheetId));
            paperSheetRepository.SaveChanges();
            return true;
        }

        public PaperSize GetPaperSheetById(int id)
        {
            return paperSheetRepository.Find(id);
        }

        #endregion

    }
}
