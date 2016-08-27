using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.RequestModels
{
    public class TemplateListRequestModel
    {
        public long ParentCategoryId { get; set; }
        public long CategoryId { get; set; }
        public string SearchString { get; set; }
        public long StoreId { get; set; }
        public bool IsPdf { get; set; }
    }
}
