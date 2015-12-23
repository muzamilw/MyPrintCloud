using System;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class DeliveryNotesService : IDeliveryNotesService
    {
        #region Private

        public readonly IDeliveryNoteRepository deliveryNoteRepository;
        private readonly ISectionFlagRepository _sectionFlagRepository;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly IDeliveryNoteDetailRepository deliveryNoteDetailRepository;
        private readonly IDeliveryCarrierRepository deliveryCarrierRepository;
        /// <summary>
        /// Creates New Pre Payment
        /// </summary>
        private DeliveryNoteDetail CreateDeliveryNoteDetail()
        {
            DeliveryNoteDetail itemTarget = deliveryNoteDetailRepository.Create();
            deliveryNoteDetailRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Pre Payment
        /// </summary>
        private void DeleteDeliveryNoteDetail(DeliveryNoteDetail prePayment)
        {
            deliveryNoteDetailRepository.Delete(prePayment);
        }

        private DeliveryNote GetById(int deliveryNoteId)
        {
            return deliveryNoteRepository.Find(deliveryNoteId);
        }

        /// <summary>
        /// Creates New Order and assigns new generated code
        /// </summary>
        private DeliveryNote CreateNewDeliveryNote()
        {
            string deliveryNoteCode = prefixRepository.GetNextDeliveryNoteCodePrefix();
            DeliveryNote itemTarget = deliveryNoteRepository.Create();
            deliveryNoteRepository.Add(itemTarget);
            itemTarget.CreationDateTime = DateTime.Now;
            itemTarget.Code = deliveryNoteCode;
            itemTarget.OrganisationId = deliveryNoteRepository.OrganisationId;
            return itemTarget;
        }

        public DeliveryNotesService(IDeliveryNoteRepository deliveryNoteRepository, ISectionFlagRepository sectionFlagRepository,
            ISystemUserRepository systemUserRepository, IPrefixRepository prefixRepository, IDeliveryNoteDetailRepository deliveryNoteDetailRepository,
            IDeliveryCarrierRepository deliveryCarrierRepository)
        {
            this.deliveryNoteRepository = deliveryNoteRepository;
            this.prefixRepository = prefixRepository;
            this.deliveryNoteDetailRepository = deliveryNoteDetailRepository;
            this.deliveryCarrierRepository = deliveryCarrierRepository;
            this._sectionFlagRepository = sectionFlagRepository;
            this._systemUserRepository = systemUserRepository;
        }

        #endregion

        #region Public
        public GetDeliveryNoteResponse GetDeliveryNotes(DeliveryNotesRequest request)
        {
            return deliveryNoteRepository.GetDeliveryNotes(request);
        }

        /// <summary>
        /// Get details of delivery note
        /// </summary>
        /// <returns></returns>
        public DeliveryNote GetDetailDeliveryNote(int id)
        {
            return deliveryNoteRepository.Find(id);
        }


        /// <summary>
        /// base Data for delivery NOte
        /// </summary>
        /// <returns></returns>
        public DeliveryNoteBaseResponse GetBaseData()
        {
            return new DeliveryNoteBaseResponse
            {
                SectionFlags = _sectionFlagRepository.GetSectionFlagBySectionId((int)SectionEnum.Delivery),
                SystemUsers = _systemUserRepository.GetAll(),
                DeliveryCarriers = deliveryCarrierRepository.GetAll(),
            };
        }


        /// <summary>
        /// Save Delivery Note
        /// </summary>
        public DeliveryNote SaveDeliveryNote(DeliveryNote deliveryNote)
        {

            deliveryNote.OrganisationId = deliveryNoteRepository.OrganisationId;
            // Get Order if exists else create new
            DeliveryNote deliveryNoteTarget = GetById(deliveryNote.DeliveryNoteId) ?? CreateNewDeliveryNote();
            // Update Order
            deliveryNote.UpdateTo(deliveryNoteTarget, new DeliveryNoteMapperAction()
            {
                CreateDeliveryNoteDetail = CreateDeliveryNoteDetail,
                DeleteDeliveryNoteDetail = DeleteDeliveryNoteDetail,

            });

            deliveryNoteRepository.SaveChanges();
            return deliveryNoteRepository.Find(deliveryNoteTarget.DeliveryNoteId);
        }


        /// <summary>
        /// Delete Delivery Note
        /// </summary>
        public void DeleteDeliveryNote(int deliveryNoteId)
        {
            DeliveryNote deliveryNote = deliveryNoteRepository.Find(deliveryNoteId);
            deliveryNoteRepository.Delete(deliveryNote);
            deliveryNoteRepository.SaveChanges();
        }

        #endregion

    }
}
