﻿using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Prefix Repository 
    /// </summary>
    public interface IPrefixRepository : IBaseRepository<Prefix, long>
    {

        /// <summary>
        /// Get Prefixed Next Job Code
        /// </summary>
        string GetNextJobCodePrefix(bool shouldIncrementNextItem = true);

        /// <summary>
        /// Get Prefixed Next Order Code
        /// </summary>
        string GetNextOrderCodePrefix();

        /// <summary>
        /// Get Prefixed Next Item Code
        /// </summary>
        string GetNextItemCodePrefix(bool shouldIncrementNextItem = true);

        Prefix GetDefaultPrefix();

        /// <summary>
        /// Markup use in Prefix 
        /// </summary>
        bool PrefixUseMarkupId(long markupId);

        List<Prefix> GetPrefixesByOrganisationID(long organisationID);

        Prefix GetPrefixByOrganisationId(long OrgId);
    }
}
