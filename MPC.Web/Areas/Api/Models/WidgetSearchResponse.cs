using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class WidgetSearchResponse
    {
        public IEnumerable<Widget> OrganisationWidgets { get; set; }
    }
}