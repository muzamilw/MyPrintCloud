using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    public class CompanyTerritory
    {
        public long TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public long? CompanyId { get; set; }
        public string TerritoryCode { get; set; }
        public bool? isDefault { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        [NotMapped]
        public IEnumerable<ScopeVariable> ScopeVariables { get; set; }


        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(CompanyTerritory target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.TerritoryName = TerritoryName;
            target.TerritoryCode = TerritoryCode;
            target.isDefault = isDefault;



        }

        #endregion
    }
}
