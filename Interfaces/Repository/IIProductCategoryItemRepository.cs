using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Product Category Item Repository 
    /// </summary>
    public interface IProductCategoryItemRepository : IBaseRepository<ProductCategoryItem, long>
    {
        long? GetCategoryId(long ItemId);
        
    }
}
