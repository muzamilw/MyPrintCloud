using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IPaperSheetRepository: IBaseRepository<PaperSize,long>
    {
        PaperSheetResponse SearchPaperSheet(PaperSheetRequestModel request);
    }
}
