using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Data;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DbConnectionFactory _connectionFactory;
    private readonly StoreContext _context;

    public CustomerRepository(DbConnectionFactory connectionFactory, StoreContext context)
    {
        _connectionFactory = connectionFactory;
        _context = context;
    }

    public async Task<IEnumerable<CustomerPrediction>> GetCustomerPredictionsAsync(string? customerName = null)
    {
        try
        {
            var query = @"
                WITH OrderDifferences AS (
                    SELECT
                        o.custid,
                        o.orderdate,
                        LAG(o.orderdate) OVER (PARTITION BY o.custid ORDER BY o.orderdate) AS prev_orderdate
                    FROM Sales.Orders o
                ),
                DateDiffs AS (
                    SELECT
                        custid,
                        DATEDIFF(DAY, prev_orderdate, orderdate) AS days_between
                    FROM OrderDifferences
                    WHERE prev_orderdate IS NOT NULL
                ),
                AvgDateDiff AS (
                    SELECT
                        custid,
                        AVG(days_between) AS avg_days_between_orders
                    FROM DateDiffs
                    GROUP BY custid
                ),
                LastOrders AS (
                    SELECT
                        custid,
                        MAX(orderdate) AS last_order_date
                    FROM Sales.Orders
                    GROUP BY custid
                ),
                Prediction AS (
                    SELECT
                        lo.custid,
                        lo.last_order_date,
                        DATEADD(DAY, ISNULL(ad.avg_days_between_orders, 0), lo.last_order_date) AS next_predicted_order
                    FROM LastOrders lo
                    INNER JOIN AvgDateDiff ad ON lo.custid = ad.custid
                )
                SELECT
                    c.custid AS CustID,
                    c.companyname AS CustomerName,
                    p.last_order_date AS LastOrderDate,
                    p.next_predicted_order AS NextPredictedOrder
                FROM Prediction p
                JOIN Sales.Customers c ON p.custid = c.custid";

            if (!string.IsNullOrEmpty(customerName))
            {
                query += " WHERE c.companyname LIKE @CustomerName";
            }

            query += " ORDER BY NextPredictedOrder";

            using var connection = _connectionFactory.CreateConnection();
            
            if (!string.IsNullOrEmpty(customerName))
            {
                var searchPattern = $"%{customerName.Trim()}%";
                var result = await connection.QueryAsync<CustomerPrediction>(query, new { CustomerName = searchPattern });
                return result;
            }
            else
            {
                var result = await connection.QueryAsync<CustomerPrediction>(query);
                return result;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetCustomerPredictionsAsync: {ex.Message}");
            return new List<CustomerPrediction>();
        }
    }
    
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        try
        {
            return await _context.Customers.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetAllCustomersAsync: {ex.Message}");
            return new List<Customer>();
        }
    }
    
    public async Task<Customer?> GetCustomerByIdAsync(int CustID)
    {
        try
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.CustID == CustID);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetCustomerByIdAsync: {ex.Message}");
            return null;
        }
    }
}
