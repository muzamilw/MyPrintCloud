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

        public DeliveryCarrier Add(DeliveryCarrier adddeliverycarriers)
        {
            _deliveryCarrierRepository.Add(adddeliverycarriers);
            _deliveryCarrierRepository.SaveChanges();

            return adddeliverycarriers;
        }

        public DeliveryCarrier Update(DeliveryCarrier upddeliverycarriers)
        {
            _deliveryCarrierRepository.Update(upddeliverycarriers);
            _deliveryCarrierRepository.SaveChanges();

            return upddeliverycarriers;
        }

        public IEnumerable<DeliveryCarrier> GetAllDeliveryCarrier()
        {

            IEnumerable<DeliveryCarrier> objenumrable;
            objenumrable = _deliveryCarrierRepository.GetAll();

            return objenumrable;
        }

    }


}
