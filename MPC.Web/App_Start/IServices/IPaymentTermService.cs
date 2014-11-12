using System.Collections.Generic;
using System.Linq;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Payment Term Service Interface
    /// </summary>
    public interface IPaymentTermService
    {
        IEnumerable<PaymentTerm> LoadAll();
    }
}
