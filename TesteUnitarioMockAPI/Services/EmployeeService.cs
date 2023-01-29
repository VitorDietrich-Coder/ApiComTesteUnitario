
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteUnitarioMockAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace TesteUnitarioMockAPI.Services
{
    public class EmployeeService : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> GetEmployeebyId(int Id)
        {
            var nome = await _context.caduser.Where(c => c.Id == Id).Select(d => d.Nome).FirstOrDefaultAsync();
            return nome;
        }

        public async Task<Boolean> GetEmployeeDetails(int Id)
        {
            var emp = await _context.caduser.FirstOrDefaultAsync(c => c.Id == Id);
            if (string.IsNullOrEmpty(emp.ToString()))
                return false;
            else
                return true;
        }
    
    }
}
