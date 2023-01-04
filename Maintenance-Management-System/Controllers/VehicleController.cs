using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Project.Controllers
{
    public class VehicleController : Controller
    {
        private readonly BLM19417EContext _context;

        public VehicleController(BLM19417EContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
          

            var custList = (from cust in _context.Customers
                           join vhc in _context.Vehicles on cust.CustomerId equals vhc.VehicleId

                           select
                           new SelectListItem()
                           {
                               Text = cust.CName + "" + cust.CLastName + "id-" + cust.CustomerId + "-",
                               Value = cust.CustomerId.ToString()
                           }

                            ).ToList();

            custList.Insert(0, new SelectListItem()
            {

                Text = "      ",
                Value = String.Empty
            });

            //ViewBag.custId= custList[1].Text;

            ViewBag.ListofCust = custList;

         
            return View(await _context.Vehicles.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {

            var custId = (from cust in _context.Customers

                          select
                          new SelectListItem()
                          {
                              
                              Value = cust.CustomerId.ToString()
                          }

                            ).ToList();

            custId.Insert(0, new SelectListItem()
            {

                Value =  String.Empty
            });

            ViewBag.custId = custId;
            ViewBag.Message = "Vehicle Added System";
            return View();
        }


        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,Model,Date,CustomerId,Plaka")] Vehicle vehiclee)
        {

            if (_context.Vehicles.Any(t => t.VehicleId == vehiclee.VehicleId))
            {
                ViewBag.Message = "Vehicle Already Exist System";
            }
            else
            {

                if (ModelState.IsValid)
                {
                    _context.Add(vehiclee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(vehiclee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,Model,Date,CustomerId,Plaka")] Vehicle vehiclee)
        {
           // vehiclee.CustomerId = vehiclee.VehicleId;
            if (id != vehiclee.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiclee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists((int)vehiclee.VehicleId))
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
            return View(vehiclee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'BLM19417EContext.Vehicles'  is null.");
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            
            var cust = await _context.Customers.FindAsync(id);
            var mnt = await _context.Maintenances.FindAsync(id);

            var generalT = await _context.GeneralTables.FindAsync(id);


            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }
            
            if (cust != null)
            {
                _context.Customers.Remove(cust);

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

     

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }


    }
}
