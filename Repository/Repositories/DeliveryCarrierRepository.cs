using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Repository.BaseRepository;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using Microsoft.Practices.Unity;
using System.Data.Entity;

namespace MPC.Repository.Repositories
{
    public class DeliveryCarrierRepository : BaseRepository<DeliveryCarrier>, IDeliveryCarrierRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DeliveryCarrierRepository(IUnityContainer container)
            : base(container)
        {

        }

        #endregion
        protected override IDbSet<DeliveryCarrier> DbSet
        {
            get
            {
                return db.DeliveryCarriers;
            }
        }

        public override IEnumerable<DeliveryCarrier> GetAll()
        {
            return DbSet.ToList();
        }

        public bool AddDeliveryCarrier(DeliveryCarrier deliveryc)
        {

            db.DeliveryCarriers.Add(deliveryc);
            db.SaveChanges();

            return true;

        }


    }
}
