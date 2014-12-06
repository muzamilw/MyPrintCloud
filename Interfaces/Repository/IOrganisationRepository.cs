using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{

    /// <summary>
    /// Company Sites Repository Interface
    /// </summary>
    public interface IOrganisationRepository : IBaseRepository<Organisation, long>
    {
         Organisation GetOrganizatiobByID(int OrganizationID);
    }
}
