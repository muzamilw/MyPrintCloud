using System;
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

        string InsertOrganisation(long OID, ExportOrganisation objExpCorporate, ExportOrganisation objExpRetail, bool isCorpStore, ExportSets Sets,string SubDomain, string TimeLog);
        Organisation GetCompanySiteDataWithTaxes();

        void DeleteOrganisationBySP(long OrganisationID);

        double GetBleedSize(long OrganisationID);

        bool GetImpericalFlagbyOrganisationId();
        void UpdateOrganisationLicensing(long organisationId, int storesCount, bool isTrial, int MisOrdersCount, int WebStoreOrdersCount, DateTime billingDate);
        void UpdateOrganisationZapTargetUrl(long organisationId, string sTargetUrl, int zapTargetType);
        void UnSubscribeZapTargetUrl(long organisationId, string sTargetUrl, int zapTargetType);
    }
}
