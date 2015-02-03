using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;

namespace MPC.Implementation.MISServices
{
    public class CompanyContactService : ICompanyContactService
    {
         private readonly ICompanyContactRepository companyContactRepository;
        #region Constructor

         public CompanyContactService(ICompanyContactRepository companyContactRepository)
        {
            this.companyContactRepository = companyContactRepository;
        }
        #endregion

        public bool Delete(long companyContactId)
        {
            var dbCompanyContact = companyContactRepository.GetContactByID(companyContactId);
            if (dbCompanyContact != null )
            {
                companyContactRepository.Delete(dbCompanyContact);
                companyContactRepository.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
