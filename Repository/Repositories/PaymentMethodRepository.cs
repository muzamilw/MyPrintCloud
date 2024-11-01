﻿using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PaymentMethod> DbSet
        {
            get
            {
                return db.PaymentMethods;
            }
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        public override IEnumerable<PaymentMethod> GetAll()
        {
            return DbSet.Where(pm => pm.IsActive == true);
        }
    }
}
