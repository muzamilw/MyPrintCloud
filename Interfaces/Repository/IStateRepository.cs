using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// State Repository Interface
    /// </summary>
    public interface IStateRepository : IBaseRepository<State, long>
    {
        List<State> GetStates();

        State GetStateFromStateID(long StateID);
    }
}
