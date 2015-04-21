using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ReportMapper
    {

        public static DomainModels.ReportCategory CreateFrom(this ReportCategory source)
        {
            return new DomainModels.ReportCategory
            {
                CategoryId = source.CategoryId,
                CategoryName = source.CategoryName,
                Description = source.Description,
                 Reports = source.Reports != null ? source.Reports.Select(g => g.CreateFrom()).ToList()  : null
                
            };
        }
        public static ReportCategory CreateFrom(this DomainModels.ReportCategory source)
        {
            return new ReportCategory
            {
                CategoryId = source.CategoryId,
                CategoryName = source.CategoryName,
                Description = source.Description,
                Reports = source.Reports != null ? source.Reports.Select(g => g.CreateFrom()).ToList() : null

            };
        }





         public static DomainModels.Report CreateFrom(this Report source)
        {
            return new DomainModels.Report
            {
                ReportId = source.ReportId,
                Name = source.Name

            };
        }
         public static Report CreateFrom(this DomainModels.Report source)
         {
             return new Report
             {
                 ReportId = source.ReportId,
                 Name = source.Name

             };
         }

    }



}