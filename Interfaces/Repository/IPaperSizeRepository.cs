using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Paper Size Repository Interface
    /// </summary>
    public interface IPaperSizeRepository : IBaseRepository<PaperSize, long>
    {
        PaperSheetResponse SearchPaperSheet(PaperSheetRequestModel request);

        List<PaperSize> GetPaperByOrganisation(long OrganisationID);

        List<PaperSize> GetPaperSizesByID(int PSSID);
        string GetPaperNameById(int sizeId);
    }
}
