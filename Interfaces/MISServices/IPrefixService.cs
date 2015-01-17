using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.MISServices
{
    public interface IPrefixService
    {
        /// <summary>
        /// Add New Prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Prefix Add(Prefix prefix);
        /// <summary>
        /// Update Prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Prefix Update(Prefix prefix);
        /// <summary>
        /// Delete Prefix
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// Get Prefixes by Organisation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Prefix GetPrefixByOrganisationId();
    }
}
