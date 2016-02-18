using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Company Cost Centre Domain Model
    /// </summary>
    public class CompanyCostCentre
    {
        public long CompanyCostCenterId { get; set; }
        public long? CompanyId { get; set; }
        public long? CostCentreId { get; set; }
        public double? BrokerMarkup { get; set; }
        public double? ContactMarkup { get; set; }
        public bool? isDisplayToUser { get; set; }
        public long? OrganisationId { get; set; }
        public virtual Company Company { get; set; }
        public virtual CostCentre CostCentre { get; set; }
        [NotMapped]
        public string CostCentreName { get; set; }
        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(CompanyCostCentre target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.CostCentreId = CostCentreId;
            target.BrokerMarkup = BrokerMarkup;
            target.ContactMarkup = ContactMarkup;
            target.isDisplayToUser = isDisplayToUser;
            target.OrganisationId = OrganisationId;
            


        }

        #endregion
    }
}
