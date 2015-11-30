using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
       public  class ListingBulletPointsRepository : BaseRepository<ListingBulletPoint>, IListingBulletPointsRepository
      {
           public ListingBulletPointsRepository(IUnityContainer container)
            : base(container)
           {

            }
           protected override IDbSet<ListingBulletPoint> DbSet
           {
               get
               {
                   return db.ListingBulletPoints;
               }
           }
         public  void UpdateBulletPoints(List<ListingBulletPoint> BulletPoints)
         {
             foreach (var item in BulletPoints)
                 {
                     ListingBulletPoint oBullet =db.ListingBulletPoints.Where(c => c.BulletPointId == item.BulletPointId).FirstOrDefault();
                     if (oBullet != null)
                     {
                         oBullet.BulletPoint = item.BulletPoint;
                         //db.Entry(oBullet).State =EntityState.Modified;
                         db.SaveChanges();
                     }
                 }
         }

         public  void AddBulletPoint(ListPointsModel model, long listingId)
         {
                 foreach (var item in model.objPointList)
                 {
                     ListingBulletPoint oBullet = new ListingBulletPoint();
                     oBullet.BulletPoint = item.BulletPoint;
                     oBullet.ListingId = listingId;
                     db.ListingBulletPoints.Add(oBullet);
                     db.SaveChanges();
                 }

             }
         
       }
}
