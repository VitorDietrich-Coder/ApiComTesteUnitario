
using Moq;
using TesteUnitarioMockAPI.Model;
using Microsoft.EntityFrameworkCore;
using TesteUnitarioMockAPI.Services;
using Xunit;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using TesteUnitarioMockAPI.Controller;

namespace UnitTesting
{
    public class EmployeeTest
    {
        #region Property  
        public Mock<IEmployeeRepository> mock = new Mock<IEmployeeRepository>();
        #endregion
        [Fact]
        public async void GetEmployeeDetailsTest()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();
            var employeeDTO = new Usuario()
            {
                Id = 114,
                Nome = "Daniel",
                Email = "v@FFF",
                Telefone = "123",
                CpfCnpj = "1231",
                Senha = "202cb962ac59075b964b07152d234b70"
            };

            mock.Setup(p => p.GetEmployeeDetails(employeeDTO.Id));
            EmployeesController employees = new EmployeesController();
            var result = employees.Details(employeeDTO.Id);
            Assert.False(employeeDTO.Equals(result));

        }
    }
}
