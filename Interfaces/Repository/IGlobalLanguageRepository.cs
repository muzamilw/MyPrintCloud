﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IGlobalLanguageRepository : IBaseRepository<GlobalLanguage, long>
    {
        string GetLanguageCodeById(long organisationId);
    }
}