using System.Collections.Generic;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Business Partner Company Service Interface
    /// </summary>
    public interface IBusinessPartnerCompanyService
    {
        /// <summary>
        /// Get All Business Partner Companies
        /// </summary>
        IEnumerable<BusinessPartnerCompany> LoadAll();
    }
}
