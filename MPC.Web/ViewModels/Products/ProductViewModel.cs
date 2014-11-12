using System.Collections;
using System.Web.Mvc;
using Models.RequestModels;
using PagedList;

namespace IstMvcFramework.ViewModels.Products
{
    public class ProductViewModel
    {

        public IPagedList<Models.Product> ProductList { get; set; }
        public IEnumerable Categories { get; set; }
        public SelectList Products { get; set; } //you can assume its a list of sub-categories
        public int? TotalPrice { get; set; }
        public int? TotalNoOfRec { get; set; }
        public ProductSearchRequest ProductSearchRequest { get; set; }
        public Models.Product Product { get; set; }


    }
}
