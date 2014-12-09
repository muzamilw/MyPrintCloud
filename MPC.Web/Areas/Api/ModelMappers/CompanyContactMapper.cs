using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Company Contact Maper
    /// </summary>
    public static class CompanyContactMapper
    {
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CompanyContact CreateFromSupplier(this CompanyContact source)
        {
            return new DomainModels.CompanyContact
            {
                ContactId = 0,
                FirstName = source.FirstName,
                LastName = source.LastName,
                HomeAddress1 = source.HomeAddress1,
                HomeCity = source.HomeCity,
                HomeState = source.HomeState,
                Mobile = source.Mobile,
                Notes = source.Notes,
                HomeTel1 = source.HomeTel1,
                HomeTel2 = source.HomeTel2,
                URL = source.URL,
                HomeExtension1 = source.HomeExtension1,
                HomeExtension2 = source.HomeExtension2,
                Email = source.Email,
                Password = source.Password,
                SecretQuestion = source.SecretQuestion,
                SecretAnswer = source.SecretAnswer,
                IsEmailSubscription = source.IsEmailSubscription,
                IsNewsLetterSubscription = source.IsNewsLetterSubscription,
            };
        }
    }
}