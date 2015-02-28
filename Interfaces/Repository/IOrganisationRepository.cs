using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{

    /// <summary>
    /// Company Sites Repository Interface
    /// </summary>
    public interface IOrganisationRepository : IBaseRepository<Organisation, long>
    {
        Organisation GetOrganizatiobByID();
        Organisation GetOrganizatiobByID(long organisationId);

        Organisation GetOrganizatiobByOrganisationID(long organisationId);

        void InsertOrganisation(long OID, ExportOrganisation objExpOrg, ExportOrganisation objExpCorporate, ExportOrganisation objExpRetail, bool isCorpStore);
        Organisation GetCompanySiteDataWithTaxes();

    }
}
