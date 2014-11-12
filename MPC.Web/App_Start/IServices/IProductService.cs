using System.Collections.Generic;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    public interface IProductService
    {
        ProductResponse LoadAllProducts(ProductSearchRequest productSearchRequest);
        Product FindProduct(int id);
        IEnumerable<Product> FindProductsByCategory(int catId); 
        void DeleteProduct(Product product);
        bool AddProduct(Product product);
        bool Update(Product product);//,Category category

    }
}
