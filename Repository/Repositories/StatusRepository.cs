using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    class StatusRepository : BaseRepository<Status>, IStatusRepository
    {


        public StatusRepository(IUnityContainer container)
            : base(container)
        {
        
        }

        protected override IDbSet<Status> DbSet
        {
            get
            {
                return db.Statuses;
            }
        }
        public List<Status> GetStatusListByStatusTypeID(int statusTypeID)
        {
            return db.Statuses.Where(sts => sts.StatusType == statusTypeID).ToList();
        }

    }
}
