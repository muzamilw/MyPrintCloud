using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class CompanyContactService : ICompanyContactService
    {
         private readonly ICompanyContactRepository companyContactRepository;
         private CompanyContact Create(CompanyContact companyContact)
         {
             companyContactRepository.Add(companyContact);
             companyContactRepository.SaveChanges();
             return companyContact;
         }
         private CompanyContact Update(CompanyContact companyContact)
         {
             companyContactRepository.Update(companyContact);
             companyContactRepository.SaveChanges();
             return companyContact;
         }
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
        public CompanyContact Save(CompanyContact companyContact)
        {
            if (companyContact.ContactId == 0)
            {
                return Create(companyContact);
            }
            return Update(companyContact);
        }
    }
}
