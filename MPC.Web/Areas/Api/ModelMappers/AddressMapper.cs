using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Address Mapper
    /// </summary>
    public static class AddressMapper
    {
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Address CreateFromSupplier(this Address source)
        {
            return new DomainModels.Address
            {
                AddressId = 0,
                AddressName = source.AddressName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                City = source.City,
                CountryId = source.CountryId,
                Tel1 = source.Tel1,
                Tel2 = source.Tel2,
                Fax = source.Fax,
                URL = source.URL,
                StateId = source.StateId,
                PostCode = source.PostCode,
                Extension1 = source.Extension1,
                Extension2 = source.Extension2,
                Email = source.Email,
                Reference = source.Reference,
                FAO = source.FAO,
            };
        }

        public static Address CreateFrom(this DomainModels.Address source)
        {
            return new Address
                   {
                       AddressId = source.AddressId,
                       CompanyId = source.CompanyId,
                       AddressName = source.AddressName,
                       Address1 = source.Address1,
                       Address2 = source.Address2,
                       Address3 = source.Address3,
                       City = source.City,
                       StateId = source.StateId,
                       StateName = source.State != null ? source.State.StateName : string.Empty,
                       StateCode = source.State != null ? source.State.StateCode : string.Empty,
                       CountryId = source.CountryId,
                       CountryName = source.Country != null ? source.Country.CountryName : string.Empty,
                       PostCode = source.PostCode,
                       Fax = source.Fax,
                       Email = source.Email,
                       URL = source.URL,
                       Tel1 = source.Tel1,
                       Tel2 = source.Tel2,
                       Extension1 = source.Extension1,
                       Extension2 = source.Extension2,
                       Reference = source.Reference,
                       FAO = source.FAO,
                       IsDefaultAddress = source.IsDefaultAddress,
                       IsDefaultShippingAddress = source.IsDefaultShippingAddress,
                       isArchived = source.isArchived,
                       TerritoryId = source.TerritoryId,
                       GeoLatitude = source.GeoLatitude,
                       GeoLongitude = source.GeoLongitude,
                       isPrivate = source.isPrivate,
                       ContactId = source.ContactId,
                       isDefaultTerrorityBilling = source.isDefaultTerrorityBilling,
                       isDefaultTerrorityShipping = source.isDefaultTerrorityShipping,
                       OrganisationId = source.OrganisationId
                   };
        }
        public static DomainModels.Address CreateFrom(this Address source)
        {
            return new DomainModels.Address
            {
                AddressId = source.AddressId,
                CompanyId = source.CompanyId,
                AddressName = source.AddressName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                City = source.City,
                StateId = source.StateId,
                CountryId = source.CountryId,
                PostCode = source.PostCode,
                Fax = source.Fax,
                Email = source.Email,
                URL = source.URL,
                Tel1 = source.Tel1,
                Tel2 = source.Tel2,
                Extension1 = source.Extension1,
                Extension2 = source.Extension2,
                Reference = source.Reference,
                FAO = source.FAO,
                IsDefaultAddress = source.IsDefaultAddress,
                IsDefaultShippingAddress = source.IsDefaultShippingAddress,
                isArchived = source.isArchived,
                TerritoryId = source.TerritoryId,
                GeoLatitude = source.GeoLatitude,
                GeoLongitude = source.GeoLongitude,
                isPrivate = source.isPrivate,
                ContactId = source.ContactId,
                isDefaultTerrorityBilling = source.isDefaultTerrorityBilling,
                isDefaultTerrorityShipping = source.isDefaultTerrorityShipping,
                OrganisationId = source.OrganisationId,
                ScopeVariables = source.ScopeVariables != null ? source.ScopeVariables.Select(sv => sv.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From For Order
        /// </summary>
        public static AddressDropDown CreateFromForOrder(this DomainModels.Address source)
        {
            return new AddressDropDown
            {
                AddressId = source.AddressId,
                AddressName = source.AddressName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Tel1 = source.Tel1,
                IsDefaultAddress = source.IsDefaultAddress
            };
        }

    }
}