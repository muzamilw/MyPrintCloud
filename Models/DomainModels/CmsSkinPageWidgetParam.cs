using System;

namespace MPC.Models.DomainModels
{
    public class CmsSkinPageWidgetParam
    {
        public long PageWidgetParamId { get; set; }
        public long? PageWidgetId { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }

        public virtual CmsSkinPageWidget CmsSkinPageWidget { get; set; }


        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(CmsSkinPageWidgetParam target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.ParamName = ParamName;
            target.ParamValue = ParamValue;



        }

        #endregion
    }
}
