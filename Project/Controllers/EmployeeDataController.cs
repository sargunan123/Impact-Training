using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Controllers
{
    public class EmployeeDataController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeDataController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: EmployeeData
        public async Task<IActionResult> Index()
        {
              return _context.EmployeesData != null ? 
                          View(await _context.EmployeesData.ToListAsync()) :
                          Problem("Entity set 'EmployeeContext.EmployeesData'  is null.");
        }
         public async Task<IActionResult> Indexes()
        {
              return _context.EmployeesData != null ? 
                          View(await _context.EmployeesData.ToListAsync()) :
                          Problem("Entity set 'EmployeeContext.EmployeesData'  is null.");
        }

        // GET: EmployeeData/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.EmployeesData == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeesData
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (employeeData == null)
            {
                return NotFound();
            }

            return View(employeeData);
        }

        // GET: EmployeeData/Create
        public IActionResult Create()
        {
            return View();
        }
       

        // POST: EmployeeData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,UserId,Password,Gender,DateofBirth,EmployeePackage,TeamName,Destination,Experience,MobileNumber,AddarNumber,Address")] EmployeeData employeeData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeData);
        }

        // GET: EmployeeData/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.EmployeesData == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeesData.FindAsync(id);
            if (employeeData == null)
            {
                return NotFound();
            }
            return View(employeeData);
        }

        // POST: EmployeeData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,UserId,Password,Gender,DateofBirth,EmployeePackage,TeamName,Destination,Experience,MobileNumber,AddarNumber,Address")] EmployeeData employeeData)
        {
            if (id != employeeData.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDataExists(employeeData.UserId))
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
            return View(employeeData);
        }

        // GET: EmployeeData/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.EmployeesData == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeesData
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (employeeData == null)
            {
                return NotFound();
            }

            return View(employeeData);
        }

        // POST: EmployeeData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.EmployeesData == null)
            {
                return Problem("Entity set 'EmployeeContext.EmployeesData'  is null.");
            }
            var employeeData = await _context.EmployeesData.FindAsync(id);
            if (employeeData != null)
            {
                _context.EmployeesData.Remove(employeeData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDataExists(string id)
        {
          return (_context.EmployeesData?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
