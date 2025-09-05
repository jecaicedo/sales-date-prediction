using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
    }
}