using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyContactRepository : IBaseRepository<CompanyContact, long>
    {
        CompanyContact GetContactUser(string email, string password);
        CompanyContact GetContactByFirstName(string Fname);
        CompanyContact GetContactByEmail(string Email);
        Int64 CreateContact(CompanyContact Contact, string Name, int OrganizationID, int CustomerType, string TwitterScreanName);

        CompanyContact CreateCorporateContact(int CustomerId, CompanyContact regContact, string TwitterScreenName);
        CompanyContact GetContactByID(Int64 ContactID);

        Models.ResponseModels.CompanyContactResponse GetCompanyContacts(
            Models.RequestModels.CompanyContactRequestModel request);

    }
}
