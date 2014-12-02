﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory, long>
    {
        List<ProductCategory> GetParentCategoriesByTerritory(long companyId);
    }
}