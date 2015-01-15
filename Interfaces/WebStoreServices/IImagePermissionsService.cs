using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IImagePermissionsService
    {
        bool UpdateImagTerritories(long imgID, string territory);
        List<ImagePermission> getTerritories(long imgID);
    }
}
