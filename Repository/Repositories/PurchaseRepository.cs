using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MPC.Repository.Repositories
{
    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(IUnityContainer container)
            : base(container)
        {
        }
        protected override IDbSet<Purchase> DbSet
        {
            get
            {
                return db.Purchases;
            }
        }
    }
}
