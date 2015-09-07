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
    public class CampaignImageRepository : BaseRepository<CampaignImage>, ICampaignImageRepository
    {

          public static int CountOfEmailsFailed = 0;


          public CampaignImageRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
          protected override IDbSet<CampaignImage> DbSet
        {
            get
            {
                return db.CampaignImages;
            }
        }
    }
}
