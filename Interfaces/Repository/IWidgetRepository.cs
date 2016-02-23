using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Widget Repository
    /// </summary>
    public interface IWidgetRepository : IBaseRepository<Widget, long>
    {
        IEnumerable<Widget> GetWidgetsByOrganisation();
        Widget GetWidgetById(long widgetId);
    }
}
