using DatabaseLab.Models;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Data;
using MvcCheckBoxList.Model;


namespace DatabaseLab.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly BLM19417EContext _context;

        public MaintenanceController(BLM19417EContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? StudentIdFk)
        {
           
            var BLM19417EContext = StudentIdFk != null ?
                _context.Maintenances.Include(t => t.StudentIdFkNavigation).Where(t => t.VehicleId == StudentIdFk):
                 _context.Maintenances.Include(t => t.StudentIdFkNavigation);

            return View(await BLM19417EContext.ToListAsync());


        }

        // GET: Takes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var take = await _context.Maintenances
                
                .Include(t => t.StudentIdFkNavigation)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (take == null)
            {
                return NotFound();
            }

            return View(take);
        }

        public IActionResult Create()
        {
           
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "FullName");
          
            return View();
        }
        
        /*
        public IActionResult Create1(int id)
        {
           

            var vehicle = _context.Vehicles.FirstOrDefault(t => t.VehicleId == id);
            var list = new List<Vehicle>() { vehicle };
            
            ViewData["VehicleId"] = new SelectList(list, "VehicleId", "FullName");
            return View(nameof(Create));
        }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,Bakım,Bakım2,Bakım3")] Maintenance maintenance)
        {
            
         //   if (ModelState.IsValid)
           // {
                
                _context.Add(maintenance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { VehicleId = maintenance.VehicleId });
           // }
            
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "FullName", maintenance.VehicleId);
          //  print(ViewData["VehicleId"]);
            return View(maintenance);
        }



        // GET: Takes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var take = await _context.Maintenances
                .Include(t => t.StudentIdFkNavigation)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (take == null)
            {
                return NotFound();
            }

            return View(take);
        }


        // POST: Takes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var take = await _context.Maintenances.FindAsync(id);
            _context.Maintenances.Remove(take);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Takes/Details/5
        public async Task<IActionResult> Calculate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var take = await _context.Maintenances        
                .Include(t => t.StudentIdFkNavigation)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (take == null)
            {
                return NotFound();
            }
            take.TotalCost = (double)(take.Bakım + take.Bakım2 + take.Bakım3);
            ViewBag.cost = take.TotalCost;

            return View(take);
        }


        /*
        public async Task<IActionResult> CalculateCost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           

            var take = await _context.Maintenances
                .Include(t => t.StudentIdFkNavigation)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            var sum = (double)(take.Bakım + take.Bakım2 + take.Bakım3);
            ViewBag.cost = sum;
            if (take == null)
            {
                return NotFound();
            }

            return View(sum);
        }
        */

        public async Task<IActionResult> Edit(int? id)
        {
           

            if (id == null || _context.Maintenances == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenances.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }


            return View(maintenance);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,Bakım,Bakım2,Bakım3")] Maintenance maintenance)
        {

            

            if (id != maintenance.VehicleId)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
          //  {
                try
                {         
                    _context.Update(maintenance);


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(maintenance.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
        //    }
            return View(maintenance);
        }







        private bool EmployeeExists(int id)
        {
            return _context.Maintenances.Any(e => e.VehicleId == id);
        }





    }


}
