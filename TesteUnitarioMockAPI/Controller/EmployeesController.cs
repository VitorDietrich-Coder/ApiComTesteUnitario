using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger.Annotations;
using TesteUnitarioMockAPI.Model;
using TesteUnitarioMockAPI.Services;

namespace TesteUnitarioMockAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        public EmployeesController(IEmployeeRepository @object)
        {
        }

        // GET: Employees/Details/5
        [HttpGet("Details/{id}")]
        [SwaggerOperation("GetCustomer")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.caduser.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return (IActionResult)employee;
        }

        // GET: Employees/Create
        [HttpGet]
        [SwaggerOperation("GetCustomer4")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Create()
        {
            return NotFound();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Employee")]
        [SwaggerOperation("PostCustomer")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Telefone,CpfCnpj,Senha")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return CreatedAtAction("Index", new {id = employee.Id}, employee);
        }

        // GET: Employees/Edit/5
        [HttpGet("{id}")]
        [SwaggerOperation("GetCustomer")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.caduser.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return (IActionResult)employee;
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [SwaggerOperation("PostCustomer5")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone,CpfCnpj,Senha")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return (IActionResult)employee;
        }

        // GET: Employees/Delete/5
        [HttpDelete("Delete/{id}")]
        [SwaggerOperation("DeleteCustomer")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.caduser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return (IActionResult)employee;
        }

        // POST: Employees/Delete/5
        [HttpDelete("DeleteConfirmed/{id}")]
        [SwaggerOperation("DeleteCustomer2")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.caduser.FindAsync(id);
            _context.caduser.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.caduser.Any(e => e.Id == id);
        }
    }
}
