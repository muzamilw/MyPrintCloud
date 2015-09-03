using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Address Domain Model
    /// </summary>
    public class Address
    {
        public long AddressId { get; set; }
        public long? CompanyId { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string PostCode { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Extension1 { get; set; }
        public string Extension2 { get; set; }
        public string Reference { get; set; }
        public string FAO { get; set; }
        public bool? IsDefaultAddress { get; set; }
        public bool? IsDefaultShippingAddress { get; set; }
        public bool? isArchived { get; set; }
        public long? TerritoryId { get; set; }
        public string GeoLatitude { get; set; }
        public string GeoLongitude { get; set; }
        public bool? isPrivate { get; set; }
        public long? ContactId { get; set; }
        public bool? isDefaultTerrorityBilling { get; set; }
        public bool? isDefaultTerrorityShipping { get; set; }
        public long? OrganisationId { get; set; }
        public bool? DisplayOnContactUs { get; set; }
        public virtual CompanyTerritory CompanyTerritory { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        public virtual ICollection<CompanyContact> ShippingCompanyContacts { get; set; }
        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<ShippingInformation> ShippingInformations { get; set; }

        #region Additional Properties

        [NotMapped]
        public List<ScopeVariable> ScopeVariables { get; set; }

        #endregion

        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(Address target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }



            target.AddressName = AddressName;
            target.Address1 = Address1;
            target.Address2 = Address2;
            target.Address3 = Address3;
            target.City = City;
            target.StateId = StateId;
          target.CountryId = CountryId;
            target.PostCode = PostCode;
            target.Fax = Fax;
          target.Email = Email;
            target.URL = URL;
            target.Tel1 = Tel1;
          target.Tel2 = Tel2;
            target.Extension1 = Extension1;
          target.Extension2 = Extension2;
            target.Reference = Reference;

            target.FAO = FAO;
            target.IsDefaultAddress = IsDefaultAddress;
          target.IsDefaultShippingAddress = IsDefaultShippingAddress;
          target.isArchived = isArchived;
          target.GeoLatitude = GeoLatitude;
          target.GeoLongitude = GeoLongitude;
         target.isPrivate = isPrivate;
         target.ContactId = ContactId;
         target.isDefaultTerrorityBilling = isDefaultTerrorityBilling;
         target.isDefaultTerrorityShipping = isDefaultTerrorityShipping;
         target.OrganisationId = OrganisationId;
         target.DisplayOnContactUs = DisplayOnContactUs;


        }

        #endregion
    }
}
