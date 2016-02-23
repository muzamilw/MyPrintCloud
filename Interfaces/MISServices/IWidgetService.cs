using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface IWidgetService
    {
        Widget GetWidgetById(long widgetId);
        IEnumerable<Widget> GetWidgetsByOrganisation();
        Widget SaveWidget(Widget widget);
        void DeleteWidget(long widgetId);
    }
}
