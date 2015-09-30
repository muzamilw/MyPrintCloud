using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class RealEstateVariableIconeListViewResponse
    {
       
            /// <summary>
            /// Row Count
            /// </summary>
            public int RowCount { get; set; }

            /// <summary>
            /// List of listing
            /// </summary>
            public IEnumerable<vw_CompanyVariableIcons> RealEstatesVariableIcons { get; set; }
        
    }
}