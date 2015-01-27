using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.RequestModels
{
    public class ProductCategoryRequestModel
    {
        public int ProductCategoryId { get; set; }
        public bool IsProductCategoryEditting { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
