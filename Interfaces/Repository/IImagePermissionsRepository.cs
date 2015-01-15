using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IImagePermissionsRepository : IBaseRepository<ImagePermission, int>
    {
        bool UpdateImagTerritories(long imgID, string[] territories);
        List<ImagePermission> getTerritories(long imgID);
    }
}
