using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class ProductCategoryFileTableViewRepository : BaseRepository<CategoryFileTableView>, IProductCategoryFileTableViewRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductCategoryFileTableViewRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CategoryFileTableView> DbSet
        {
            get
            {
                return db.CategoryFileTableViews;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find File in File Table
        /// </summary>
        public CategoryFileTableView Find(Guid id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get File by StreamId
        /// </summary>
        public CategoryFileTableView GetByStreamId(Guid streamId)
        {
            return DbSet.FirstOrDefault(file => file.StreamId == streamId);
        }

        /// <summary>
        /// Returns new path for directory/file
        /// </summary>
        public string GetNewPathLocator(string path, string fileTableName)
        {
            return db.GetNewPathLocator(path, fileTableName);
        }

        #endregion
    }
}
