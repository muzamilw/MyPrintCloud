﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class RealEstateVariableIconsListViewResponse
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
