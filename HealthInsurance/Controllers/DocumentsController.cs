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
    public class DocumentsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public DocumentsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.Document.Include(d => d.InsuranceProducts);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Document == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .Include(d => d.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.DocId == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocId,DocName,DocFile,ProductId")] Document document, IFormFile docFile)
        {
            if (docFile != null && docFile.Length > 0)
            {
                var fileName = Path.GetFileName(docFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/files", fileName);
                document.DocFile = fileName;
                _context.Add(document);
                await _context.SaveChangesAsync();

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await docFile.CopyToAsync(fileStream);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", document.ProductId);
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Document == null)
            {
                return NotFound();
            }

            var document = await _context.Document.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", document.ProductId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocId,DocName,DocFile,ProductId")] Document document)
        {
            if (id != document.DocId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.DocId))
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
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", document.ProductId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Document == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .Include(d => d.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.DocId == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Document == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.Document'  is null.");
            }
            var document = await _context.Document.FindAsync(id);
            if (document != null)
            {
                _context.Document.Remove(document);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
          return (_context.Document?.Any(e => e.DocId == id)).GetValueOrDefault();
        }
    }
}
