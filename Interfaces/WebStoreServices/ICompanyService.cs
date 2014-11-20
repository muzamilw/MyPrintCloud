using MPC.Models.DomainModels;

namespace MPC.Interfaces.WebStoreServices
{
    /// <summary>
    /// My Organization Service Interface
    /// </summary>
    public interface ICompanyService
    {

        Company GetCompanyByDomain(string domain);
    }
}
