using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using System.Linq;

namespace MPC.Repository.Repositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Currency> DbSet
        {
            get
            {
                  return db.Currencies;
            }
        }

        public string GetCurrencyCodeById(long currencyId)
        {
            return db.Currencies.Where(c => c.CurrencyId == currencyId).Select(n => n.CurrencyCode).FirstOrDefault();
        }


        public string GetCurrencySymbolById(long currencyId)
        {
            Currency oCurr = db.Currencies.Where(c => c.CurrencyId == currencyId).FirstOrDefault();
            if (oCurr != null)
            {
                return oCurr.CurrencySymbol;
            }
            else 
            {
                return "";
            }
            
        }

        public Currency GetCurrencySymbolByOrganisationId(long OrganisationID)
        {

            long currencyId = db.Organisations.Where(c => c.OrganisationId == OrganisationID).Select(c => c.CurrencyId ?? 0).FirstOrDefault();
            return db.Currencies.Where(c => c.CurrencyId == currencyId).FirstOrDefault();
        }

      
    }
}
