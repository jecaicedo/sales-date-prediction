using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Data;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StoreContext _context;
        private readonly DbConnectionFactory _connectionFactory;

        public EmployeeRepository(StoreContext context, DbConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int empId)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.EmpID == empId);
        }
    }
}