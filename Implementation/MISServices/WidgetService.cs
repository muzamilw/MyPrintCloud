using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.MISServices
{
    class WidgetService : IWidgetService
    {

        private readonly IWidgetRepository widgetRepository;

        public WidgetService(IWidgetRepository widgetRepository)
        {
            if (widgetRepository == null)
            {
                throw new ArgumentNullException("widgetRepository");
            }
            this.widgetRepository = widgetRepository;
        }

        public Widget GetWidgetById(long widgetId)
        {
            return widgetRepository.GetWidgetById(widgetId);
        }

        public IEnumerable<Widget> GetWidgetsByOrganisation()
        {
            return widgetRepository.GetWidgetsByOrganisation();
        }

        public Widget SaveWidget(Widget widget)
        {
            try
            {
                Widget targetWidget = GetWidgetById(widget.WidgetId) ?? CreateNewWidget();
                UpdateWidget(targetWidget, widget);
                widgetRepository.SaveChanges();
                return targetWidget;
            }
            catch (Exception exp)
            {
                throw new MPCException("Failed to save widgets. Error: " + exp.Message, widgetRepository.OrganisationId);
            }
            
        }

        private Widget CreateNewWidget()
        {
            Widget newWidget = widgetRepository.Create();
            newWidget.OrganisationId = widgetRepository.OrganisationId;
            widgetRepository.Add(newWidget);
            return newWidget;
        }
        private static void UpdateWidget(Widget target, Widget source)
        {
            target.WidgetCode = source.WidgetCode;
            target.WidgetCss = source.WidgetCss;
            target.WidgetHtml = source.WidgetHtml;
            target.WidgetName = source.WidgetName;
        }

        public void DeleteWidget(Widget widget)
        {
            widgetRepository.Delete(widget);
            widgetRepository.SaveChanges();
        }
    }
}
