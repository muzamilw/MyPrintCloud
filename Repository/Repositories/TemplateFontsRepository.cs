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
    public class TemplateFontsRepository : BaseRepository<TemplateFont>, ITemplateFontsRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateFontsRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TemplateFont> DbSet
        {
            get
            {
                return db.TemplateFonts;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public TemplateFont Find(int id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        ///  Get template fonts list by template id and customer ID (store id) // added by saqib ali
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        /// 
        public List<TemplateFont> GetFontList(long productId, long customerId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<TemplateFont> lFont = new List<TemplateFont>();
            var res = db.sp_GetUsedFontsUpdated(productId, customerId);
            lFont = res.Select(g => new TemplateFont
            {
                ProductFontId = g.ProductFontId,
                ProductId = g.ProductId,
                FontName = g.FontName,
                FontDisplayName = g.FontDisplayName,
                FontFile = g.FontFile,
                DisplayIndex = g.DisplayIndex,
                IsPrivateFont = g.IsPrivateFont,
                IsEnable = g.IsEnable,
                CustomerId = g.CustomerID,
                FontPath = g.FontPath
            }).ToList();
            return lFont;

        }


        // delete template fonts from database against company ID // added by saqib ali
        public void DeleteTemplateFonts(long Companyid)
        {
            foreach (TemplateFont c in  db.TemplateFonts.Where(c => c.CustomerId == Companyid))
            {
                db.TemplateFonts.Remove(c);
            }
            db.SaveChanges();
        }

        // get all template fonts in db called while geerating proof // should be optimzed
        public List<TemplateFont> GetFontList()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.TemplateFonts.ToList();

        }
        public List<TemplateFont> GetFontListForTemplate(long templateId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<TemplateFont> fonts = new List<TemplateFont>();
            var item = db.Items.Where(g => g.TemplateId == templateId).SingleOrDefault();
            if(item != null)
            {
                fonts = db.TemplateFonts.Where(g => g.CustomerId == item.CompanyId || g.CustomerId == null).ToList();
            }
            return fonts;
        }
        public  void InsertFontFile(TemplateFont objFont)
        {
            db.TemplateFonts.Add(objFont);
            db.SaveChanges();
        }

        public List<TemplateFont> getTemplateFontsByCompanyID(long CustomerID)
        {
           try
           {
               return db.TemplateFonts.Where(c => c.CustomerId == CustomerID).ToList();
           }
           catch(Exception ex)
           {
               throw ex;
           }
        }

        public List<TemplateFont> getTemplateFonts()
        {
            try
            {
                return db.TemplateFonts.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
