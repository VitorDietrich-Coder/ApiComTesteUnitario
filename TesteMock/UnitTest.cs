
using Moq;
using TesteUnitarioMockAPI.Controllers;
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
        public async Task VerifyIndexActionReturnsIndexView()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();
            var employeeController = new EmployeesController(employeeRepository.Object);
            var actionResult = await employeeController.index();
            var result = actionResult as ViewResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
        }
    }
}
