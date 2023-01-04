using DatabaseLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;


namespace DatabaseLab.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BLM19417EContext _context;

        public CustomerController(BLM19417EContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            /*
            var courseManagementDBContext = _context.Customers.Include(s => s.VehicIdFkNavigation);
            return View(await courseManagementDBContext.ToListAsync());
            */
            
            return View(await _context.Customers.ToListAsync());


        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
               .FirstOrDefaultAsync(m => m.CustomerId == id);
          


            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        

        // GET: Employees/Create
        public IActionResult Create()
        {

            //   ViewData["CustomerId"] = new SelectList(_context.Vehicles, "VehicleId", "Model");

            // TempData["SuccessMessage"] = "Do you want to add details ?";

            ViewBag.Message = "Customer Added System";
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CName,CLastName,CNumber")] Customer customer, String cinsiyet)
        {
            var dbId = from cust in _context.Customers select cust.CustomerId;
            if(_context.Customers.Any(t=>t.CustomerId == customer.CustomerId))
            {
                ViewBag.Message = "Customer Id Exist System";
            }
            else {
           
            if (ModelState.IsValid)
            {
                customer.Cinsiyet = cinsiyet;
                _context.Add(customer);
                await _context.SaveChangesAsync();
           //     ViewBag.Message ="Customer Added System";
                return RedirectToAction(nameof(Index));
            }
            }
            //   ViewData["CustomerId"] = new SelectList(_context.Vehicles, "VehicleId", "Model", customer.CustomerId);
         //   ViewBag.Message = "Customer Added System";
            return View(customer);
        }
       

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            

            return View(customer);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CName,CLastName,CNumber")] Customer customer, String cinsiyet)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.Cinsiyet = cinsiyet;
                    _context.Update(customer);


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var employee = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Message = "Customer Deleted System";
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'BLM19417EContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);

            var vehicle = await _context.Vehicles.FindAsync(id);

            var mnt = await _context.Maintenances.FindAsync(id);

            var generalT = await _context.GeneralTables.FindAsync(id);


            if (customer != null)
            {
                
                _context.Customers.Remove(customer);
            }
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }
            if (mnt != null)
            {
                _context.Maintenances.Remove(mnt);
            }

            if (generalT != null)
            {
                _context.GeneralTables.Remove(generalT);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}