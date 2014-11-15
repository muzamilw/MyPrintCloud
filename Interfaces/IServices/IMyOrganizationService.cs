using System.Collections.Generic;
namespace MPC.Interfaces.IServices
{
    /// <summary>
    /// My Organization Service Interface
    /// </summary>
    public interface IMyOrganizationService
    {
        IList<int> GetOrganizationIds(int request);
    }
}
