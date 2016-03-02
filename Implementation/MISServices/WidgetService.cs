using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
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
        private readonly ICmsSkinPageWidgetRepository skinPageWidgetRepository;

        public WidgetService(IWidgetRepository widgetRepository, ICmsSkinPageWidgetRepository skinPageWidgetRepository)
        {
            if (widgetRepository == null)
            {
                throw new ArgumentNullException("widgetRepository");
            }
            if (skinPageWidgetRepository == null)
            {
                throw new ArgumentNullException("skinPageWidgetRepository");
            }
            this.widgetRepository = widgetRepository;
            this.skinPageWidgetRepository = skinPageWidgetRepository;
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

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string url = "WebstoreApi/StoreCache/Get?id=" + widget.CompanyId;
                    var response = client.GetAsync(url);
                    if (!response.Result.IsSuccessStatusCode)
                    {
                        //throw new MPCException("Failed to clear store cache", companyRepository.OrganisationId);
                    }
                }

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
            target.WidgetControlName = source.WidgetControlName;
        }

        public void DeleteWidget(long widgetId)
        {
            if (skinPageWidgetRepository.IsCustomWidgetUsed(widgetId))
                throw new MPCException("Widget cannot be deleted as it is in use.", widgetRepository.OrganisationId);
            else
            {
                var widgetToDelete = widgetRepository.GetWidgetById(widgetId);
                if (widgetToDelete != null)
                {
                    widgetRepository.Delete(widgetToDelete);
                    widgetRepository.SaveChanges();
                }
                
            }
            
        }
    }
}
