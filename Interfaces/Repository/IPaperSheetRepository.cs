using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;

namespace MPC.Interfaces.Repository
{
    public interface IPaperSheetRepository: IBaseRepository<PaperSize,long>
    {
        IEnumerable<PaperSize> SearchPaperSheet(PaperSheetRequestModel request, out int rowCount);
    }
}
