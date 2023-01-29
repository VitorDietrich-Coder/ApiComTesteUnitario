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

        // GET: Employees/index
        [HttpGet("index")]
        [SwaggerOperation("GetCustomer2")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Usuario>>> index()
        {
            return await _context.caduser.ToListAsync();
        }

        // GET: Employees/Details/5
        [HttpGet("Details/GetEmployeeDetails/{id}")]
        [SwaggerOperation("teste")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Usuario>>> Details(int? id)
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
            return await _context.caduser.ToListAsync();
        }

        // GET: Employees/Create
        [HttpGet("Create")]
        [SwaggerOperation("GetCustomer4")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Create()
        {
            return NotFound();
        }

        // POST: Employees/Create/Usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create/Usuario")]
        [SwaggerOperation("PostCustomer")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Telefone,CpfCnpj,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return CreatedAtAction("Index", new {id = usuario.Id}, usuario);
        }

        // GET: Employees/Edit/5
        [HttpGet("Edit/{id}")]
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
        [HttpPost("Edit/Usuario")]
        [SwaggerOperation("PostCustomer5")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone,CpfCnpj,Senha")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadastroUsuarioExiste(usuario.Id))
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
            return (IActionResult)usuario;
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

        private bool CadastroUsuarioExiste(int id)
        {
            return _context.caduser.Any(e => e.Id == id);
        }
    }
}
