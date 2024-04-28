using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        BankContext _context;
        public EmployeeController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Employee> GetAll()
        {
            return _context.Employees.Select(employee => new Employee
            {
                Name = employee.Name,
                CivilId = employee.CivilId,
                Position = employee.Position,
            }).ToList();
        }


        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeRequest req)
        {
            _context.Employees.Add(new Employee()
            {
                Name = req.Name,
                Position = req.Position,
                CivilId = req.CivilId,
            });
            _context.SaveChanges();
            return Created();
        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var bank = _context.Employees.Find(id);
            if (bank == null)
            {
                return NotFound();
            }
            return Ok(new EmployeeResponce
            {
                Name = bank.Name,
                CivilId = bank.CivilId,
                Position = bank.Position,
                Id = bank.Id
            });
        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, AddEmployeeRequest add)
        {
            var bank = _context.Employees.Find(id);
            add.Id = add.Id;
            add.Name = add.Name;
            add.CivilId = add.CivilId;
            add.Position = add.Position;
            _context.SaveChanges();
            return Created(nameof(Details), new { Id = add.Id });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bank = _context.Employees.Find(id);
            if (bank == null)
            {
                return BadRequest();
            }
            _context.Employees.Remove(bank);
            _context.SaveChanges();
            return Ok();
        }

    }
}
   