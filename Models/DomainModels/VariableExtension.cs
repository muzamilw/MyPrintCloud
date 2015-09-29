using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Variable Extension Domain Model
    /// </summary>
    public class VariableExtension
    {
        public int Id { get; set; }
        public long? FieldVariableId { get; set; }
        public int? CompanyId { get; set; }
        public int? OrganisationId { get; set; }
        public string VariablePrefix { get; set; }
        public string VariablePostfix { get; set; }
        public bool? CollapsePrefix { get; set; }
        public bool? CollapsePostfix { get; set; }
        public virtual FieldVariable FieldVariable { get; set; }


        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(VariableExtension target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }

            target.CompanyId = CompanyId;
            target.OrganisationId = OrganisationId;
            target.VariablePrefix = VariablePrefix;
            target.VariablePostfix = VariablePostfix;
            target.CollapsePrefix = CollapsePrefix;
            target.CollapsePostfix = CollapsePostfix;


        }

        #endregion
    }
}
