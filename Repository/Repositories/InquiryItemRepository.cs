using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class InquiryItemRepository : BaseRepository<InquiryItem>, IInquiryItemRepository
    {
        public InquiryItemRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<InquiryItem> DbSet
        {
            get { return db.InquiryItems; }
        }
    }
}
