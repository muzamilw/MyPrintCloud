using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class CmsSkinPageWidget
    {
        public long PageWidgetId { get; set; }
        public long? PageId { get; set; }
        public long? WidgetId { get; set; }
        public long? SkinId { get; set; }
        public short? Sequence { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Organisation Organisation { get; set; }
        public virtual Widget Widget { get; set; }
        public virtual CmsPage CmsPage { get; set; }
        public virtual ICollection<CmsSkinPageWidgetParam> CmsSkinPageWidgetParams { get; set; }


        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(CmsSkinPageWidget target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }

            target.WidgetId = WidgetId;
            target.SkinId = SkinId;
            target.Sequence = Sequence;



        }

        #endregion
    }
}
