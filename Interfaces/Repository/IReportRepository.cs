﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IReportRepository
    {
        List<Report> GetReportsByOrganisationID(long OrganisationID);

        List<ReportNote> GetReportNotesByOrganisationID(long OrganisationID);
    }
}
