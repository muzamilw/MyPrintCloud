using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Paper Size Repository Interface
    /// </summary>
    public interface IPaperSizeRepository : IBaseRepository<PaperSize, long>
    {
        PaperSheetResponse SearchPaperSheet(PaperSheetRequestModel request);
    }
}
