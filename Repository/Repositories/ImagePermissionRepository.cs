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
    class ImagePermissionRepository : BaseRepository<ImagePermission>, IImagePermissionsRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ImagePermissionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ImagePermission> DbSet
        {
            get
            {
                return db.ImagePermissions;
            }
        }
        #endregion

        #region public
        public ImagePermission Find(int id)
        {
            return DbSet.Find(id);
        }


        public bool UpdateImagTerritories(long imgID, string[] territories)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<ImagePermission> oldPermissions = db.ImagePermissions.Where(g => g.ImageId == imgID).ToList();
            foreach (var obj in oldPermissions)
            {
                db.ImagePermissions.Remove(obj);
            }
            foreach (string obj in territories)
            {
                if (obj != "")
                {
                    ImagePermission objPermission = new ImagePermission();
                    objPermission.ImageId = Convert.ToInt32(imgID);
                    objPermission.TerritoryID = Convert.ToInt32(obj);
                    db.ImagePermissions.Add(objPermission);
                }
            }
            db.SaveChanges();
            return true;
        }
        public List<ImagePermission> getTerritories(long imgID)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.ImagePermissions.Where(g => g.ImageId == imgID).ToList();
        }
        
        #endregion
    }
}
