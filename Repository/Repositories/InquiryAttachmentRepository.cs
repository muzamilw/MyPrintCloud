using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class InquiryAttachmentRepository: BaseRepository<InquiryAttachment>, IInquiryAttachmentRepository
    {
        public InquiryAttachmentRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<InquiryAttachment> DbSet
        {
            get
            {
                return db.InquiryAttachments;
            }
        }


        public void SaveInquiryAttachments(List<InquiryAttachment> AttachmentList) 
        {
            foreach(var attch in AttachmentList)
            {
                db.InquiryAttachments.Add(attch);
            }

            db.SaveChanges();
        }
    }
}
