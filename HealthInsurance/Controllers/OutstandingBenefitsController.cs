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
    public class OutstandingBenefitsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public OutstandingBenefitsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: OutstandingBenefits
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.OutstandingBenefit.Include(o => o.InsuranceProducts);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: OutstandingBenefits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OutstandingBenefit == null)
            {
                return NotFound();
            }

            var outstandingBenefit = await _context.OutstandingBenefit
                .Include(o => o.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.ObId == id);
            if (outstandingBenefit == null)
            {
                return NotFound();
            }

            return View(outstandingBenefit);
        }

        // GET: OutstandingBenefits/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName");
            return View();
        }

        // POST: OutstandingBenefits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObId,OdName,OdDescription,Image,ProductId")] OutstandingBenefit outstandingBenefit, IFormFile imagefile)
        {
            if (imagefile != null && imagefile.Length > 0)
            {
                var fileName = Path.GetFileName(imagefile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/images", fileName);
                outstandingBenefit.Image = fileName;
                _context.Add(outstandingBenefit);
                await _context.SaveChangesAsync();

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagefile.CopyToAsync(fileStream);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", outstandingBenefit.ProductId);
            return View(outstandingBenefit);
        }

        // GET: OutstandingBenefits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OutstandingBenefit == null)
            {
                return NotFound();
            }

            var outstandingBenefit = await _context.OutstandingBenefit.FindAsync(id);
            if (outstandingBenefit == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", outstandingBenefit.ProductId);
            return View(outstandingBenefit);
        }

        // POST: OutstandingBenefits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObId,OdName,OdDescription,Image,ProductId")] OutstandingBenefit outstandingBenefit)
        {
            if (id != outstandingBenefit.ObId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outstandingBenefit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutstandingBenefitExists(outstandingBenefit.ObId))
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
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", outstandingBenefit.ProductId);
            return View(outstandingBenefit);
        }

        // GET: OutstandingBenefits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OutstandingBenefit == null)
            {
                return NotFound();
            }

            var outstandingBenefit = await _context.OutstandingBenefit
                .Include(o => o.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.ObId == id);
            if (outstandingBenefit == null)
            {
                return NotFound();
            }

            return View(outstandingBenefit);
        }

        // POST: OutstandingBenefits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OutstandingBenefit == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.OutstandingBenefit'  is null.");
            }
            var outstandingBenefit = await _context.OutstandingBenefit.FindAsync(id);
            if (outstandingBenefit != null)
            {
                _context.OutstandingBenefit.Remove(outstandingBenefit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutstandingBenefitExists(int id)
        {
          return (_context.OutstandingBenefit?.Any(e => e.ObId == id)).GetValueOrDefault();
        }
    }
}
