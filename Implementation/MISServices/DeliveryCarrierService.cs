using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
namespace MPC.Implementation.MISServices
{
    public class DeliveryCarrierService : IDeliveryCarriersService
    {
        private readonly IDeliveryCarrierRepository _deliveryCarrierRepository;

        public DeliveryCarrierService(IDeliveryCarrierRepository deliveryCarrierRepository)
        {
            this._deliveryCarrierRepository = deliveryCarrierRepository;
        }

        public bool Add(DeliveryCarrier adddeliverycarriers)
        {
            _deliveryCarrierRepository.AddDeliveryCarrier(adddeliverycarriers);
            _deliveryCarrierRepository.SaveChanges();

            return true;
        }

        public bool Update(DeliveryCarrier upddeliverycarriers)
        {
            upddeliverycarriers.OrganisationId = _deliveryCarrierRepository.OrganisationId;
            _deliveryCarrierRepository.Update(upddeliverycarriers);
            _deliveryCarrierRepository.SaveChanges();

            return true;
        }

        public IEnumerable<DeliveryCarrier> GetAll()
        {

            IEnumerable<DeliveryCarrier> objenumrable;
            objenumrable = _deliveryCarrierRepository.GetAll();

            return objenumrable;
        }

    }


}
