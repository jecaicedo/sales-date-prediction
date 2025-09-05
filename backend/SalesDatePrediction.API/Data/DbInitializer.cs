using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                using var context = new StoreContext(
                    serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>());
                
                context.Database.EnsureCreated();
                
                if (context.Customers.Any())
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetService<ILogger<StoreContext>>();
                logger?.LogError(ex, "Error al inicializar la base de datos. La aplicación continuará funcionando con funcionalidad limitada.");
            }
        }
    }
}
