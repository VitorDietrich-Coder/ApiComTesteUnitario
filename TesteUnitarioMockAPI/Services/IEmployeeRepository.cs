
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteUnitarioMockAPI.Model;

namespace TesteUnitarioMockAPI.Services
{
    public interface IEmployeeRepository
    {
        Task<Boolean> GetEmployeeDetails(int Id);
        Task<string> GetNomeById(int Id);
    }
}
