using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyVariableIconsMapper
    {
        public static vw_CompanyVariableIcons CreateFrom(this MPC.Models.DomainModels.vw_CompanyVariableIcons source)
        {
            return new vw_CompanyVariableIcons
            {
                variableid = source.variableid,
                variablename = source.variablename,
                variabletag = source.variabletag,
                CompanyId = source.CompanyId,
                OrganisationId = source.OrganisationId,
                VariableIconId = source.VariableIconId,
                Icon = source.Icon

            };
        }

        public static MPC.Models.DomainModels.vw_CompanyVariableIcons CreateFrom(this vw_CompanyVariableIcons source)
        {
            return new MPC.Models.DomainModels.vw_CompanyVariableIcons
            {
                variableid = source.variableid,
                variablename = source.variablename,
                variabletag = source.variabletag,
                CompanyId = source.CompanyId,
                OrganisationId = source.OrganisationId,
                VariableIconId = source.VariableIconId,
                Icon = source.Icon

            };
        }


        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static RealEstateVariableIconeListViewResponse CreateFromListView(this  MPC.Models.ResponseModels.RealEstateVariableIconsListViewResponse source)
        {
            return new RealEstateVariableIconeListViewResponse
            {
                RowCount = source.RowCount,
                RealEstatesVariableIcons = source.RealEstatesVariableIcons.Select(c => c.CreateFrom())
            };
        }
    }
}