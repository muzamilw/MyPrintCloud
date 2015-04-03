
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Item Section Repository 
    /// </summary>
    public interface IItemSectionRepository : IBaseRepository<ItemSection, long>
    {
        BestPressResponse GetBestPressResponse(ItemSection section);
    }
}
