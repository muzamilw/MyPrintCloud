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
        string GetStateNameById(long StateId);

        State GetStateFromStateID(long StateID);

        string GetStateCodeById(long stateId);
        State GetStateByName(string sStateName);
    }
}
