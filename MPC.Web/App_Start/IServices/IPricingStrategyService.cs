using System.Collections.Generic;
using System.Linq;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Pricing Strategy Interface
    /// </summary>
    public interface IPricingStrategyService
    {
        IEnumerable<PricingStrategy> LoadAll();
    }
}
