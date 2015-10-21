using System;
using System.IO;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using Company = MPC.Models.DomainModels.Company;
using CompanyContact = MPC.Models.DomainModels.CompanyContact;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Customer's Mapper
    /// </summary>
    public static class CustomerMapper
    {
        #region Private

        /// <summary>
        /// Get Customer's Status
        /// </summary>
        private static string GetCustomerStatus(short statusCode)
        {
            if (statusCode == 0)
                return "Inactive";
            if (statusCode == 1)
                return "Active";
            if (statusCode == 2)
                return "Banned";
            if (statusCode == 3)
                return "Pending";
            return string.Empty;
        }
        #endregion
        #region Public
        /// <summary>
        /// Create Customer List View
        /// </summary>
        public static CustomerListViewModel CreateFromCustomer(this Company source)
        {
            byte[] bytes = null;
            string imagePath = HttpContext.Current.Server.MapPath("~/" + source.Image);
            if (source.Image != null && File.Exists(imagePath))
            {
                bytes = source.Image != null ? File.ReadAllBytes(imagePath) : null;
            }
            string defaultContact = null;
            string email = null;
           CompanyContact companyContact = source.CompanyContacts.FirstOrDefault(contact => contact.IsDefaultContact == 1);
            if (companyContact != null)
            {
                defaultContact = companyContact.FirstName + " "+ companyContact.LastName;
                email = companyContact.Email;
            }
            return new CustomerListViewModel
            {
                CustomerName = source.Name,
                DefaultContactName = defaultContact,
                DefaultContactEmail=email,
                CustomerType= source.IsCustomer,
                DateCreted = source.CreationDate,
                Email = source.MarketingBriefRecipient,
                Status = GetCustomerStatus(source.Status),
                CompnayId = source.CompanyId,
                
                Image = bytes,
                StoreImagePath = !string.IsNullOrEmpty(source.Image) ? source.Image + "?" + DateTime.Now.ToString() : string.Empty,
                StoreName = source.StoreName
            };
        }
        #endregion
    }
}