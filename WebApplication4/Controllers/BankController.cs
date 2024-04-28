using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/bank")]
    [ApiController]
    public class BankController : Controller

    {
        private readonly BankContext _context;
        public BankController(BankContext context)
        {
            _context = context;
        }
        /*
        [HttpGet]
        public List<BankBranchResponce> GetAll()
        {
            return _context.BankBranches.Select(b => new BankBranchResponce
            {
                BranchManager = b.BranchManager,
                Location = b.Location,
                Name = b.Name,
                EmployeeCount = b.EmployeeCount,

            }).ToList();
        }
        */

        [HttpGet]
        public IActionResult GetAll(string filter = "", int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<BankBranch> bankBranches = _context.BankBranches;

            if (!string.IsNullOrEmpty(filter))
            {
                bankBranches = bankBranches.Where(b => b.Name.Contains(filter) || b.Location.Contains(filter));
            }

            var pagedBankBranches = bankBranches.OrderBy(b => b.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var result = pagedBankBranches.Select(b => new BankBranchResponce
            {
                BranchManager = b.BranchManager,
                Location = b.Location,
                Name = b.Name,
                EmployeeCount = b.EmployeeCount,

            }).ToList();

            int totalCount = bankBranches.Count();
            return Ok(new { result, totalCount });
        }
        [HttpPost]
        public IActionResult AddBankRequest(AddBankRequest req)
        {
            _context.BankBranches.Add(new BankBranch()
            {
                Name = req.Name,
                Location = req.Location,
                BranchManager = req.BranchManager,
                EmployeeCount = req.EmployeeCount,
            });
            _context.SaveChanges();
            return Created();
        }

        [HttpGet("{id}")]
        public ActionResult<BankBranchResponce> Details(int id)
        {
            var bank = _context.BankBranches.Find(id);
            if (bank == null)
            {
                return NotFound();
            }
            return new BankBranchResponce
            {
                BranchManager = bank.BranchManager,
                Location = bank.Location,
                EmployeeCount = bank.EmployeeCount,
                Name = bank.Name,

            };

        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, AddBankRequest req)
        {
            var bank = _context.BankBranches.Find(id);

            bank.Name = req.Name;
            bank.Location = req.Location;
            bank.BranchManager = req.BranchManager;
            bank.EmployeeCount = req.EmployeeCount;


            _context.SaveChanges();

            return Created(nameof(Details), new { Id = bank.Id });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bank = _context.BankBranches.Find(id);
            _context.BankBranches.Remove(bank);
            _context.SaveChanges();

            return Ok();

        }
    }
}