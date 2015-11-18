using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ExportStore
    {
        public Company Company { get; set; }
        
        public List<TemplateFont> templateFonts { get; set; }


        public List<DiscountVoucher> DiscountVouchers { get; set; }

        public List<ProductCategory> StoreCategories { get; set; }
        public List<Item> StoreItems { get; set; }

        public List<CompanyContact> StoreContacts { get; set; }


    }
}
