using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Smart From Domain Model
    /// </summary>
    public class SmartForm
    {
        public long SmartFormId { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<SmartFormDetail> SmartFormDetails { get; set; }



        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(SmartForm target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.Name = Name;
            target.Heading = Heading;
            target.OrganisationId = OrganisationId;
          



        }

        #endregion
    }
}
