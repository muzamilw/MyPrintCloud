﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ITemplateFontsRepository : IBaseRepository<TemplateFont, int>
    {
        List<TemplateFont> GetFontList(int productId, int customerId);
    }
}
