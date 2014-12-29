using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICategoryService
    {
        List<ProductCategory> GetChildCategories(int categoryId);
    }
}
