﻿using MPC.Models.Common;
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

        void InsertOrganisation(long OID,ExportOrganisation objExpCorporate,ExportOrganisation objExpRetail,bool isCorpStore,ExportSets Sets);
        Organisation GetCompanySiteDataWithTaxes();

        void DeleteOrganisationBySP(long OrganisationID);

        //Estimate GetOrderByOrderID(long OrderID);
    }
}
