using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Data;
using HealthInsurance.Models;

namespace HealthInsurance.Controllers
{
    public class PackageInsurancesController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public PackageInsurancesController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: PackageInsurances
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.PackageInsurance.Include(p => p.InsuranceProducts);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: PackageInsurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PackageInsurance == null)
            {
                return NotFound();
            }

            var packageInsurance = await _context.PackageInsurance
                .Include(p => p.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (packageInsurance == null)
            {
                return NotFound();
            }

            return View(packageInsurance);
        }

        // GET: PackageInsurances/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName");
            return View();
        }

        // POST: PackageInsurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageId,PackageName,Duration,Price,Limit,ProductId")] PackageInsurance packageInsurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packageInsurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", packageInsurance.ProductId);
            return View(packageInsurance);
        }

        // GET: PackageInsurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PackageInsurance == null)
            {
                return NotFound();
            }

            var packageInsurance = await _context.PackageInsurance.FindAsync(id);
            if (packageInsurance == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", packageInsurance.ProductId);
            return View(packageInsurance);
        }

        // POST: PackageInsurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackageId,PackageName,Duration,Price,Limit,ProductId")] PackageInsurance packageInsurance)
        {
            if (id != packageInsurance.PackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packageInsurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageInsuranceExists(packageInsurance.PackageId))
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
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", packageInsurance.ProductId);
            return View(packageInsurance);
        }

        // GET: PackageInsurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PackageInsurance == null)
            {
                return NotFound();
            }

            var packageInsurance = await _context.PackageInsurance
                .Include(p => p.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (packageInsurance == null)
            {
                return NotFound();
            }

            return View(packageInsurance);
        }

        // POST: PackageInsurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PackageInsurance == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.PackageInsurance'  is null.");
            }
            var packageInsurance = await _context.PackageInsurance.FindAsync(id);
            if (packageInsurance != null)
            {
                _context.PackageInsurance.Remove(packageInsurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageInsuranceExists(int id)
        {
          return (_context.PackageInsurance?.Any(e => e.PackageId == id)).GetValueOrDefault();
        }
    }
}
