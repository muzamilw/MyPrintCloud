using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class PaperSheetService : IPaperSheetService
    {
        #region Private

        private readonly IPaperSizeRepository paperSheetRepository;
        private readonly IOrganisationRepository organisationRepository;

        #endregion

        #region Constructor

        public PaperSheetService(IPaperSizeRepository paperSheetRepository, IOrganisationRepository organisationRepository)
        {
            this.paperSheetRepository = paperSheetRepository;
            this.organisationRepository = organisationRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Paper Sheets as requested
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PaperSheetResponse GetAll(PaperSheetRequestModel request)
        {
            return paperSheetRepository.SearchPaperSheet(request);
        }
        /// <summary>
        /// Add Paper Sheet
        /// </summary>
        /// <param name="paperSize"></param>
        /// <returns></returns>
        public PaperSize Add(PaperSize paperSize)
        {
            paperSize.OrganisationId = organisationRepository.OrganisationId;
            paperSheetRepository.Add(paperSize);
            paperSheetRepository.SaveChanges();
            return paperSize;
        }

        /// <summary>
        /// Update Paper Sheet
        /// </summary>
        public PaperSize Update(PaperSize paperSize)
        {
            paperSize.OrganisationId = organisationRepository.OrganisationId;

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
            PaperSize paperSize = GetPaperSheetById(paperSheetId);
            paperSize.IsArchived = true;
            paperSheetRepository.SaveChanges();
            return true;
        }

        public PaperSize GetPaperSheetById(int id)
        {
            return paperSheetRepository.Find(id);
        }

        public PaperSheetBaseResponse GetBaseData()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            return new PaperSheetBaseResponse
            {
                LengthUnit = organisation.LengthUnit != null ? organisation.LengthUnit.UnitName : string.Empty,
                Culture = organisation.GlobalLanguage != null ? organisation.GlobalLanguage.culture : string.Empty
            };
        }
        #endregion

    }
}
