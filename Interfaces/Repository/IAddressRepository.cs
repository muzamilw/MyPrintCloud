using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IAddressRepository : IBaseRepository<Address, long>
    {
        List<Address> GetAddressesByTerritoryID(Int64 TerritoryID);
    }
}
