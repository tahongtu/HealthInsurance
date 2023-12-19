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
    public class DiseaseListsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public DiseaseListsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: DiseaseLists
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.DiseaseList.Include(d => d.InsuranceProducts);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: DiseaseLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DiseaseList == null)
            {
                return NotFound();
            }

            var diseaseList = await _context.DiseaseList
                .Include(d => d.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.DiseaseId == id);
            if (diseaseList == null)
            {
                return NotFound();
            }

            return View(diseaseList);
        }

        // GET: DiseaseLists/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName");
            return View();
        }

        // POST: DiseaseLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiseaseId,DiseaseName,Lv1,Lv2,Lv3,ProductId")] DiseaseList diseaseList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diseaseList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", diseaseList.ProductId);
            return View(diseaseList);
        }

        // GET: DiseaseLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DiseaseList == null)
            {
                return NotFound();
            }

            var diseaseList = await _context.DiseaseList.FindAsync(id);
            if (diseaseList == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", diseaseList.ProductId);
            return View(diseaseList);
        }

        // POST: DiseaseLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiseaseId,DiseaseName,Lv1,Lv2,Lv3,ProductId")] DiseaseList diseaseList)
        {
            if (id != diseaseList.DiseaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diseaseList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseListExists(diseaseList.DiseaseId))
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
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", diseaseList.ProductId);
            return View(diseaseList);
        }

        // GET: DiseaseLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DiseaseList == null)
            {
                return NotFound();
            }

            var diseaseList = await _context.DiseaseList
                .Include(d => d.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.DiseaseId == id);
            if (diseaseList == null)
            {
                return NotFound();
            }

            return View(diseaseList);
        }

        // POST: DiseaseLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DiseaseList == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.DiseaseList'  is null.");
            }
            var diseaseList = await _context.DiseaseList.FindAsync(id);
            if (diseaseList != null)
            {
                _context.DiseaseList.Remove(diseaseList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseaseListExists(int id)
        {
          return (_context.DiseaseList?.Any(e => e.DiseaseId == id)).GetValueOrDefault();
        }
    }
}
