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
    public class InquiryRepository : BaseRepository<Inquiry>, IInquiryRepository
    {
        public InquiryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Inquiry> DbSet
        {
            get
            {
                return db.Inquiries;
            }
        }
        public long AddInquiryAndItems(Inquiry Inquiry, List<InquiryItem> InquiryItems)
        {
            
            try
            {
                Prefix oPrefix =  db.Prefixes.Where(c => c.SystemSiteId == 1).FirstOrDefault();
                // Get order prefix and update the order next number

                if (oPrefix != null)
                {
                    Inquiry.InquiryCode = oPrefix.EnquiryPrefix + "-001-" + oPrefix.EnquiryNext.ToString();
                    oPrefix.EnquiryNext = oPrefix.EnquiryNext + 1;
                }

                db.Inquiries.Add(Inquiry);
                if(db.SaveChanges() > 0)
                {
                    foreach (var item in InquiryItems) 
                    {
                        item.InquiryId = Inquiry.InquiryId;
                        db.InquiryItems.Add(item);
                    }
                }

                if (db.SaveChanges() > 0)
                {
                    return Inquiry.InquiryId;
                }
                else 
                {
                    return Inquiry.InquiryId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
