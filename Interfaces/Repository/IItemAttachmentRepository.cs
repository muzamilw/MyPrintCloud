using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;


namespace MPC.Interfaces.Repository
{
    public interface IItemAttachmentRepository : IBaseRepository<ItemAttachment, long>
    {
        List<ItemAttachment> GetArtworkAttachments(long ItemId);
    }
}
