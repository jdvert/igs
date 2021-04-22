using IgsMarket.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgsMarket.Api.Services
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(int productId);
        Task<Product> UpdateProduct(Product product);
    }
}
