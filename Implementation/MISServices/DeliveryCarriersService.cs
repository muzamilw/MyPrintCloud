using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.MISServices
{
    public class DeliveryCarriersService : IDeliveryCarriersService
    {
        private readonly IDeliveryCarrierRepository _deliveryCarrierRepository;


        public DeliveryCarriersService(IDeliveryCarrierRepository deliveryCarrierRepository)
        {
            this._deliveryCarrierRepository = deliveryCarrierRepository;
        }

        public DeliveryCarrier Add(DeliveryCarrier deliverycarrier)
        {
            _deliveryCarrierRepository.Add(deliverycarrier);
            _deliveryCarrierRepository.SaveChanges();
            return deliverycarrier;
        }

        public DeliveryCarrier Update(DeliveryCarrier deliverycarrier)
        {
            _deliveryCarrierRepository.Update(deliverycarrier);
            _deliveryCarrierRepository.SaveChanges();
            return deliverycarrier;
        }
        public bool Delete(int id)
        {
            _deliveryCarrierRepository.Delete(GetDeliveryCarrierByID());
            _deliveryCarrierRepository.SaveChanges();

            return true;
        }
        public DeliveryCarrier GetDeliveryCarrierByID()
        {
            return _deliveryCarrierRepository.GetDeliveryCarrierByID(_deliveryCarrierRepository.OrganisationId);
        }

        public List<DeliveryCarrier> GetDeliveryCarrierAll()
        { 
        

            return 

        }
    }
}
