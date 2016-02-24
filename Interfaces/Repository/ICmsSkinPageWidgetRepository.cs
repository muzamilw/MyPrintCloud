using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface ICmsSkinPageWidgetRepository : IBaseRepository<CmsSkinPageWidget, long>
    {
        List<CmsSkinPageWidget> GetDomainWidgetsById(long companyId);

        /// <summary>
        /// Get By Page Id
        /// </summary>
        IEnumerable<CmsSkinPageWidget> GetByPageId(long pageId,long companyId);

        List<CmsSkinPageWidget> GetDomainWidgetsById2(long companyId);
        bool IsCustomWidgetUsed(long widgetId);
    }
}
