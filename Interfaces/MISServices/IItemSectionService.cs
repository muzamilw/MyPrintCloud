using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface IItemSectionService
    {
        PtvDTO GetPTV(PTVRequestModel request);
        PtvDTO GetPTVCalculation(PTVRequestModel request);
        BestPressResponse GetBestPressResponse(ItemSection currentSection);
        ItemSection GetUpdatedSectionWithSystemCostCenters(ItemSection currentSection);
        bool SaveItemSection(ItemSection currItemSection);
        ItemSection GetItemSectionById(long itemsectionId);
    }
}
