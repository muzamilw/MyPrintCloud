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
    public class CompanyVoucherRedeemRepository : BaseRepository<CompanyVoucherRedeem>, ICompanyVoucherRedeemRepository
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyVoucherRedeemRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CompanyVoucherRedeem> DbSet
        {
            get
            {
                return db.CompanyVoucherRedeems;
            }
        }

        #endregion
        public bool IsVoucherUsedByCustomer(long contactId, long companyId, long DiscountVoucherId)
        {
            try
            {
               CompanyVoucherRedeem vReedem = db.CompanyVoucherRedeems.Where(v => v.CompanyId == companyId && v.ContactId == contactId && v.DiscountVoucherId == DiscountVoucherId).FirstOrDefault();
               if (vReedem != null)
               {
                   return false;
               }
               else 
               {
                   return true;
               }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AddReedem(long contactId, long companyId, long DiscountVoucherId)
        {
            try
            {
                CompanyVoucherRedeem vReedem = new CompanyVoucherRedeem();
                vReedem.DiscountVoucherId = DiscountVoucherId;
                vReedem.ContactId = contactId;
                vReedem.CompanyId = companyId;
                vReedem.RedeemDate = DateTime.Now;
                db.CompanyVoucherRedeems.Add(vReedem);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
