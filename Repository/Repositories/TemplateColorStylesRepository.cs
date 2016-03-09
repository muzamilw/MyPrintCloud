using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Linq;
using System.Collections.Generic;
using System;

namespace MPC.Repository.Repositories
{
    public class TemplateColorStylesRepository : BaseRepository<TemplateColorStyle>, ITemplateColorStylesRepository
    {
        #region private
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateColorStylesRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TemplateColorStyle> DbSet
        {
            get
            {
                return db.TemplateColorStyles;
            }
        }

        #endregion
        #region public 
        /// <summary>
        /// Find Template
        /// </summary>
        public TemplateColorStyle Find(int id)
        {
            return DbSet.Find(id);
        }
        // get color styles list based on product id and customer id(corporate store id) // added by saqib ali
        public List<TemplateColorStyle> GetColorStyle(long ProductId, long CustomerId, long territoryId)
        {
          
            db.Configuration.LazyLoadingEnabled = false;
            List<TemplateColorStyle> oStyles = null;
            long? territory = null;
            if (territoryId != 0)
                territory = territoryId;
            if (CustomerId == 0)
            {
                oStyles = db.TemplateColorStyles.Where(g => (g.ProductId == ProductId || g.ProductId == null) && g.CustomerId == null && g.TerritoryId == null).ToList();
            }
            else
            {
                oStyles = db.TemplateColorStyles.Where(g => g.CustomerId == CustomerId && g.TerritoryId == territory).ToList();
            }
            return oStyles;
        }
        // get color list based on product id // added by saqib ali
        public List<TemplateColorStyle> GetColorStyle(long ProductId)
        {

            db.Configuration.LazyLoadingEnabled = false;
            List<TemplateColorStyle> oStyles = null;
            oStyles = db.TemplateColorStyles.Where(g => (g.ProductId == ProductId || g.ProductId == null) && g.TerritoryId == null).ToList();
            return oStyles;
        }
        // add new corporate color // added by saqib ali
        public string SaveCorpColor(int C, int M, int Y, int K, string Name, long CustomerID)
        {
            var color = db.TemplateColorStyles.Where(g => g.SpotColor == Name && g.CustomerId == CustomerID).FirstOrDefault();
            if (color == null)
            {
                TemplateColorStyle obj = new TemplateColorStyle();
                obj.ColorC = C;
                obj.ColorM = M;
                obj.ColorY = Y;
                obj.ColorK = K;
                obj.IsSpotColor = true;
                obj.SpotColor = Name;
                obj.Name = Name;
                obj.IsColorActive = true;
                obj.CustomerId = CustomerID;
                db.TemplateColorStyles.Add(obj);
                db.SaveChanges();
                return obj.PelleteId.ToString();
            }
            else
            {
                return "Already Exsist";
            }
         
        }
        // activate or deactivate corporate color // // added by saqib ali
        public string UpdateCorpColor(long id, string type)
        {
            string result = "";
            var obj = db.TemplateColorStyles.Where(g => g.PelleteId == id).SingleOrDefault();
            if (obj != null)
            {
                if (type == "DeActive")
                {
                    obj.IsColorActive = false;
                }
                else
                {
                    obj.IsColorActive = true;
                }

                db.SaveChanges();
                result = "saved";
            }

            return result;
           
        }

        public TemplateColorStyle ArchiveSpotColor(long SpotColorId)
        {
            try
            {
                TemplateColorStyle objColor = db.TemplateColorStyles.Where(c => c.PelleteId == SpotColorId).FirstOrDefault();
                if(objColor != null)
                {
                    objColor.IsColorActive = false;
                }
                db.SaveChanges();
                return objColor;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
