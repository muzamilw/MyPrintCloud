
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    public class ItemAddonCostCentre
    {
        public int ProductAddOnId { get; set; }
        public long? ItemStockOptionId { get; set; }
        public long? CostCentreId { get; set; }
        public double? DiscountPercentage { get; set; }
        public bool? IsDiscounted { get; set; }
        public int? Sequence { get; set; }
        public bool? IsMandatory { get; set; }

        public virtual CostCentre CostCentre { get; set; }

        public virtual ItemStockOption ItemStockOption { get; set; }
        [NotMapped]
        public string CostCentreName { get; set; }

        #region Public

        /// <summary>
        /// Create Clone of Item Stock Option
        /// </summary>
        public void Clone(ItemAddonCostCentre target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemAddonCostCentre_InvalidItem, "target");
            }

            target.CostCentreId = CostCentreId;
            target.IsMandatory = IsMandatory;

        }

        #endregion
    }
}
