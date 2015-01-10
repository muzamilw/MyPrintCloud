using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Prefix Repository 
    /// </summary>
    public interface IPrefixRepository : IBaseRepository<Prefix, long>
    {
        /// <summary>
        /// Get Prefixed Next Item Code
        /// </summary>
        string GetNextItemCodePrefix();

        Prefix GetDefaultPrefix();

        /// <summary>
        /// Markup use in Prefix 
        /// </summary>
        bool PrefixUseMarkupId(long markupId);
    }
}
