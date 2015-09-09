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
using MPC.Models.Common;

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
        public List<ItemAttachment> GetItemAttactchments(long itemID)
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

        public List<ArtWorkAttatchment> GetItemAttactchmentsForRegenerateTemplateAttachments(long itemID, string fileExtionsion, UploadFileTypes uploadedFileType)
        {

            string uploadFiType = uploadedFileType.ToString();
            List<ArtWorkAttatchment> itemAttactchments = new List<ArtWorkAttatchment>();



            var query = from Attachment in db.ItemAttachments
                        where Attachment.ItemId == itemID &&
                              string.Compare(Attachment.Type, uploadFiType, true) == 0 &&
                              string.Compare(Attachment.FileType, fileExtionsion, true) == 0
                        select new ArtWorkAttatchment()
                        {
                            FileName = Attachment.FileName,
                            FileTitle = Attachment.FileTitle,
                            FileExtention = Attachment.FileType,
                            FolderPath = Attachment.FolderPath,
                        };

            itemAttactchments = query.ToList<ArtWorkAttatchment>();



            if (itemAttactchments != null && itemAttactchments.Count > 0)
                itemAttactchments.ForEach(att => att.UploadFileType = uploadedFileType);


            return itemAttactchments;
        }



        public ItemAttachment PopulueTblItemAttachment(long itemID, long customerID, long? contactId, string fileTitle, string fileName, UploadFileTypes type, string fileExtention, string folderPath)
        {
            ItemAttachment attchment = new ItemAttachment
            {
                ItemId = itemID,
                FileTitle = fileTitle,
                FileType = fileExtention,
                Type = type.ToString(),
                FileName = fileName,
                FolderPath = folderPath,
                CompanyId = customerID,
                ContactId = contactId,
                IsApproved = 1,
                UploadDate = DateTime.Now,
                UploadTime = DateTime.Now,
                isFromCustomer = 1

            };

            return attchment;

        }

        /// <summary>
        /// gets the single attachment record
        /// </summary>
        /// <param name="AttachmentId"></param>
        /// <returns></returns>
        public ItemAttachment GetArtworkAttachment(long AttachmentId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.ItemAttachments.Where(a => a.ItemAttachmentId == AttachmentId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// delete attachment
        /// </summary>
        /// <param name="AttachmentId"></param>
        /// <returns></returns>
        public void DeleteArtworkAttachment(ItemAttachment AttachmentRecord)
        {
            try
            {
                db.ItemAttachments.Remove(AttachmentRecord);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Get Item Attachments By Ids
        /// </summary>
        public List<ItemAttachment> GetItemAttachmentsByIds(List<long?> itemIds)
        {
            Expression<Func<ItemAttachment, bool>> query =
                  attch =>
                      (itemIds.Contains(attch.ItemId));

            return DbSet.Where(query)
                        .ToList();
        }

        

    }
}
