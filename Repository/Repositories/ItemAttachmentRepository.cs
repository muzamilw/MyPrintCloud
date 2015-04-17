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
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.ItemAttachments.Where(a => a.ItemId == ItemId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ItemAttachment> SaveArtworkAttachments(List<ItemAttachment> attachmentList)
        {
            try
            {
                long itemID = 0;
                foreach (var attachment in attachmentList)
                {
                    db.ItemAttachments.Add(attachment);
                    itemID = attachment.ItemId ?? 0;
                }

                if (db.SaveChanges() > 0)
                {
                    return db.ItemAttachments.Where(i => i.ItemId == itemID).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  List<ItemAttachment> GetItemAttactchments(long itemID)
        {
            try
            {
                return (from Attachment in db.ItemAttachments
                        where Attachment.ItemId == itemID
                        select Attachment).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
