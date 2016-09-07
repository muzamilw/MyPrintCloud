using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Markup Repository Interface
    /// </summary>
    public interface IMarkupRepository : IBaseRepository<Markup, long>
    {
        Markup GetZeroMarkup();

        List<Markup> GetMarkupsByOrganisationId(long OID);

        string GetMarkupNamebyID(long ID);

        List<Markup> GetMarkups();
        Markup GetDefaultMarkupsByOrganisationId(long OID);
        long GetOrganisationDefaultMarkupId();
    }
}
