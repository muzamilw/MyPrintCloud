using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class ProductTemplateListResponseModel
    {
        public List<ProductCategory> ParentCategories { get; set; }
        public List<ProductCategory> SubCategories { get; set; }
        public List<usp_GetStoreProductTemplatesList_Result> ProductTemplateList { get; set; }
    }
}
