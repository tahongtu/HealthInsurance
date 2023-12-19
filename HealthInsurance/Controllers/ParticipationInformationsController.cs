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
    public class ParticipationInformationsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public ParticipationInformationsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: ParticipationInformations
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.ParticipationInformation.Include(p => p.InsuranceProducts);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: ParticipationInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParticipationInformation == null)
            {
                return NotFound();
            }

            var participationInformation = await _context.ParticipationInformation
                .Include(p => p.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.ParticipationId == id);
            if (participationInformation == null)
            {
                return NotFound();
            }

            return View(participationInformation);
        }

        // GET: ParticipationInformations/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName");
            return View();
        }

        // POST: ParticipationInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParticipationId,PIName,PIDescription,File,ProductId")] ParticipationInformation participationInformation, IFormFile filedoc)
        {
            if (filedoc != null && filedoc.Length > 0)
            {
                var fileName = Path.GetFileName(filedoc.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/files", fileName);
                participationInformation.File = fileName;

                _context.Add(participationInformation);
                await _context.SaveChangesAsync();

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await filedoc.CopyToAsync(fileStream);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", participationInformation.ProductId);
            return View(participationInformation);
        }

        // GET: ParticipationInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParticipationInformation == null)
            {
                return NotFound();
            }

            var participationInformation = await _context.ParticipationInformation.FindAsync(id);
            if (participationInformation == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", participationInformation.ProductId);
            return View(participationInformation);
        }

        // POST: ParticipationInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParticipationId,PIName,PIDescription,File,ProductId")] ParticipationInformation participationInformation)
        {
            if (id != participationInformation.ParticipationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participationInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipationInformationExists(participationInformation.ParticipationId))
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
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", participationInformation.ProductId);
            return View(participationInformation);
        }

        // GET: ParticipationInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParticipationInformation == null)
            {
                return NotFound();
            }

            var participationInformation = await _context.ParticipationInformation
                .Include(p => p.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.ParticipationId == id);
            if (participationInformation == null)
            {
                return NotFound();
            }

            return View(participationInformation);
        }

        // POST: ParticipationInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParticipationInformation == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.ParticipationInformation'  is null.");
            }
            var participationInformation = await _context.ParticipationInformation.FindAsync(id);
            if (participationInformation != null)
            {
                _context.ParticipationInformation.Remove(participationInformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipationInformationExists(int id)
        {
          return (_context.ParticipationInformation?.Any(e => e.ParticipationId == id)).GetValueOrDefault();
        }
    }
}
