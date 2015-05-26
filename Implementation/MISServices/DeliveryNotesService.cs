using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class DeliveryNotesService : IDeliveryNotesService
    {
        #region Private

        public readonly IDeliveryNoteRepository DeliveryNoteRepository;
        private readonly ISectionFlagRepository _sectionFlagRepository;
        private readonly ISystemUserRepository _systemUserRepository;


        public DeliveryNotesService(IDeliveryNoteRepository deliveryNoteRepository, ISectionFlagRepository sectionFlagRepository, ISystemUserRepository systemUserRepository)
        {
            DeliveryNoteRepository = deliveryNoteRepository;
            this._sectionFlagRepository = sectionFlagRepository;
            this._systemUserRepository = systemUserRepository;
        }

        #endregion
        #region Public
        public GetDeliveryNoteResponse GetDeliveryNotes(DeliveryNotesRequest request)
        {
          return  DeliveryNoteRepository.GetDeliveryNotes(request);
        }

        /// <summary>
        /// Get details of delivery note
        /// </summary>
        /// <returns></returns>
        public DeliveryNote  GetDetailDeliveryNote(int id)
        {
           return DeliveryNoteRepository.Find(id);
        }


        /// <summary>
        /// base Data for delivery NOte
        /// </summary>
        /// <returns></returns>
        public DeliveryNoteBaseResponse GetBaseData()
        {
            return new DeliveryNoteBaseResponse
            {
                SectionFlags = _sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Order),
                SystemUsers = _systemUserRepository.GetAll(),
            };
        }
        #endregion
    
    }
}
