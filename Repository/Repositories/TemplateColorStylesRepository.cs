using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Linq;
using System.Collections.Generic;

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
        public List<TemplateColorStyle> GetColorStyle(int ProductId, int CustomerId)
        {
          
            db.Configuration.LazyLoadingEnabled = false;
            List<TemplateColorStyle> oStyles = null;
            if (CustomerId == 0)
            {
                oStyles = db.TemplateColorStyles.Where(g => (g.ProductId == ProductId || g.ProductId == null) && g.CustomerId == null).ToList();
            }
            else
            {
                oStyles = db.TemplateColorStyles.Where(g => g.CustomerId == CustomerId).ToList();
            }
            return oStyles;
        }
        public List<TemplateColorStyle> GetColorStyle(int ProductId)
        {

            db.Configuration.LazyLoadingEnabled = false;
            List<TemplateColorStyle> oStyles = null;
            oStyles = db.TemplateColorStyles.Where(g => g.ProductId == ProductId || g.ProductId == null).ToList();
            return oStyles;
        }
        public int SaveCorpColor(int C, int M, int Y, int K, string Name, int CustomerID)
        {
            TemplateColorStyle obj = new TemplateColorStyle();
            obj.ColorC =C;
            obj.ColorM = M;
            obj.ColorY = Y;
            obj.ColorK = K;
            obj.IsSpotColor = true;
            obj.SpotColor = Name;
            obj.IsColorActive = true;
            obj.CustomerId = CustomerID;

            db.TemplateColorStyles.Add(obj);
            db.SaveChanges();
            return (int)obj.PelleteId;
        }
        public string UpdateCorpColor(int id, string type)
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
        #endregion
    }
}
