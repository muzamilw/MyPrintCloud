﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface ISystemUserService
    {
        bool Update(System.Guid Id, string Email, string FullName, int OrganizationId, int status, string EmailSignature, string EstimateHeadNotes, string EstimateFootNotes);
        bool Add(System.Guid Id, string Email, string FullName, int OrganizationId);
        void UpdateEmailSignature(string signature);
        string GetEmailSignature();
    }
}
