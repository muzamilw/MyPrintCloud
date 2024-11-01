﻿using Microsoft.Practices.Unity;
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
    public class TemplateObjectRepository: BaseRepository<TemplateObject>, ITemplateObjectRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateObjectRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TemplateObject> DbSet
        {
            get
            {
                return db.TemplateObjects;
            }
        }

        #endregion

        #region public
        /// <summary>
        /// Find TemplatePage
        /// </summary>
        public TemplateObject Find(int id)
        {
            return DbSet.Find(id);
        }
        //get list of template objects // added by saqib ali

        public List<TemplateObject> GetProductObjects(long productId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var result = db.TemplateObjects.Where(g => g.ProductId == productId).ToList();
            result = result.OrderBy(g => g.DisplayOrderPdf).ToList();
            result = result.OrderBy(g => g.ProductPageId).ToList();
            return result;
        }

        /// <summary>
        /// Get Template Objects by Tempalte Pages list
        /// </summary>
        public IEnumerable<TemplateObject> GetByTemplatePages(IEnumerable<long?> templatePages)
        {
            if (templatePages == null)
            {
                return new List<TemplateObject>();
            }

            List<long?> templatePageList = templatePages.Where(tp => tp.HasValue && tp.Value > 0).ToList();
            return DbSet.Where(tempObj => templatePageList.Contains(tempObj.ProductPageId)).ToList();
        }

        #endregion

        
    }
}
