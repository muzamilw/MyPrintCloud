using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Variable
    /// </summary>
    public class TemplateVariable
    {
        public long ProductVariableId { get; set; }
        public long? TemplateId { get; set; }
        public long? VariableId { get; set; }
        public virtual FieldVariable FieldVariable { get; set; }



        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(TemplateVariable target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.TemplateId = TemplateId;
           


        }

        #endregion
    }
}
