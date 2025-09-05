using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
    }
}