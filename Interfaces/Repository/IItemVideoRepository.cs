using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Item Video Repository 
    /// </summary>
    public interface IItemVideoRepository : IBaseRepository<ItemVideo, long>
    {
        List<ItemVideo> GetProductVideos(long ItemID);
    }
}
