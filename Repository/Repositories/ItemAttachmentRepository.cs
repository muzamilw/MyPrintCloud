using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MPC.Repository.Repositories
{
    public class ItemAttachmentRepository : BaseRepository<ItemAttachment>, IItemAttachmentRepository
    {
        public ItemAttachmentRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemAttachment> DbSet
        {
            get
            {
                return db.ItemAttachments;
            }
        }

        public List<ItemAttachment> GetArtworkAttachments(long ItemId)
        {
            return db.ItemAttachments.Where(a => a.ItemId == ItemId).ToList();
        }
    }
}
