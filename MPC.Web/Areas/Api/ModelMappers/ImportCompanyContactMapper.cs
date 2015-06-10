using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ImportCompanyContactMapper
    {
        /// <summary>
        /// Company Contact Mapper Web Model -> Domain Model
        /// </summary>
        public static StagingImportCompanyContactAddress Createfrom(this ImportCompanyContact source, long companyId)
        {
            return new StagingImportCompanyContactAddress
            {
                //CompanyName = source.CompanyName,
                CompanyId = companyId,
                //AddressId = source.AddressId,
                AddressName = source.AddressName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                //Address3 = source.Address3,
                City = source.City,
                State = source.State,
                //StateId = source.StateId,
                Country = source.Country,
                //CountryId = source.CountryId,
                Postcode = source.Postcode,
                //TerritoryId = source.TerritoryId,
                TerritoryName = source.TerritoryName,
                AddressPhone = source.AddressPhone,
                AddressFax = source.AddressFax,
                //ContactId = source.ContactId,
                ContactFirstName = source.ContactFirstName,
                ContactLastName = source.ContactLastName,
                JobTitle = source.JobTitle,
                Email = source.Email,
                //password = source.password,
                Mobile = source.Mobile,
                //RoleId = source.RoleId,
                ContactPhone = source.ContactPhone,
                ContactFax = source.ContactFax,
                AddInfo1 = source.AddInfo1,
                AddInfo2 = source.AddInfo2,
                AddInfo3 = source.AddInfo3,
                AddInfo4 = source.AddInfo4,
                AddInfo5 = source.AddInfo5,
                //OrganisationId = source.OrganisationId,
            };
        }
    }
}