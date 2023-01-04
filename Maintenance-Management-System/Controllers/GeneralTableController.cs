using DatabaseLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLab.Controllers
{
    public class GeneralTableController : Controller
    {
        private readonly BLM19417EContext _context;

        public GeneralTableController(BLM19417EContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? StudentIdFk)
        {
            var BLM19417EContext = StudentIdFk != null ?
                _context.GeneralTables.Include(t => t.MaintenanceNavigation).Include(t => t.VehicleNavigation).Include(t => t.CustomerNavigation).Where(t => t.Id == StudentIdFk) :
                 _context.GeneralTables.Include(t => t.MaintenanceNavigation).Include(t => t.VehicleNavigation).Include(t => t.CustomerNavigation);

            ViewBag.Message = "Customer Will be Delete To Table";
            return View(await BLM19417EContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SearchCustomer(string search)
        {
            var courseManagementDBContext = _context.GeneralTables.Include(c => c.MaintenanceNavigation).Include(c => c.VehicleNavigation).Include(c => c.CustomerNavigation).Where(t => t.CustomerNavigation.CName.ToLower().Contains(search.ToLower()));
            return View(nameof(Index), await courseManagementDBContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Message = "Customer Added to Table";
            ViewData["Id"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");

            return View();
        }

        public IActionResult Create1(int id)
        {
            var student = _context.Customers.FirstOrDefault(t => t.CustomerId == id);
            var list = new List<Customer>() { student };
           // ViewData["Id"] = new SelectList(_context.Courses.Where(t => t.DeptIdFk == student.DeptIdFk), "Id", "Name");
            ViewData["Id"] = new SelectList(list, "CustomerId", "CustomerId");
            return View(nameof(Create));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] GeneralTable generalTable)
        {
            if (_context.GeneralTables.Any(t => t.CustomerNavigation.CustomerId == generalTable.Id))
            {
                ViewBag.Message = "Customer Id Exist Table";
            }
            else {
               
                _context.Add(generalTable);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { Id = generalTable.Id });
             }

            ViewData["Id"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", generalTable.Id);
            return View(generalTable);
        }

      
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.GeneralTables == null)
            {
                return Problem("Entity set 'BLM19417EContext.Employees'  is null.");
            }
            var employee = await _context.GeneralTables.FindAsync(id);
            if (employee != null)
            {
                _context.GeneralTables.Remove(employee);
            }

            await _context.SaveChangesAsync();
           
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.GeneralTables.Any(e => e.Id == id);
        }


    }
}
