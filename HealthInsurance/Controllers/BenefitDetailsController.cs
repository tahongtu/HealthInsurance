using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Data;
using HealthInsurance.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HealthInsurance.Controllers
{
    public class BenefitDetailsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public BenefitDetailsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: BenefitDetails
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.BenefitDetail.Include(b => b.InsuranceProducts);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: BenefitDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BenefitDetail == null)
            {
                return NotFound();
            }

            var benefitDetail = await _context.BenefitDetail
                .Include(b => b.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.BenefitDetailId == id);
            if (benefitDetail == null)
            {
                return NotFound();
            }

            return View(benefitDetail);
        }

        // GET: BenefitDetails/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName");
            return View();
        }

        // POST: BenefitDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BenefitDetailId,Title,Content,Image,ProductId")] BenefitDetail benefitDetail, IFormFile imagefile)
        {
            if (imagefile != null && imagefile.Length > 0)
            {
                var fileName = Path.GetFileName(imagefile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/images", fileName);

                using (var image = Image.Load(imagefile.OpenReadStream()))
                {
                    var newWidth = 600;
                    var newHeight = 600;

                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(newWidth, newHeight),
                        Mode = ResizeMode.Max
                    }));
                    image.Save(filePath); // Lưu ảnh đã resize vào đường dẫn

                    benefitDetail.Image = fileName;
                    _context.Add(benefitDetail);
                    await _context.SaveChangesAsync();
                }

                
                

                //using (var fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    await imagefile.CopyToAsync(fileStream);
                //}
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", benefitDetail.ProductId);
            return View(benefitDetail);
        }

        // GET: BenefitDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BenefitDetail == null)
            {
                return NotFound();
            }

            var benefitDetail = await _context.BenefitDetail.FindAsync(id);
            if (benefitDetail == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", benefitDetail.ProductId);
            return View(benefitDetail);
        }

        // POST: BenefitDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BenefitDetailId,Title,Content,Image,ProductId")] BenefitDetail benefitDetail)
        {
            if (id != benefitDetail.BenefitDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(benefitDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenefitDetailExists(benefitDetail.BenefitDetailId))
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
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "ProductName", benefitDetail.ProductId);
            return View(benefitDetail);
        }

        // GET: BenefitDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BenefitDetail == null)
            {
                return NotFound();
            }

            var benefitDetail = await _context.BenefitDetail
                .Include(b => b.InsuranceProducts)
                .FirstOrDefaultAsync(m => m.BenefitDetailId == id);
            if (benefitDetail == null)
            {
                return NotFound();
            }

            return View(benefitDetail);
        }

        // POST: BenefitDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BenefitDetail == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.BenefitDetail'  is null.");
            }
            var benefitDetail = await _context.BenefitDetail.FindAsync(id);
            if (benefitDetail != null)
            {
                _context.BenefitDetail.Remove(benefitDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenefitDetailExists(int id)
        {
          return (_context.BenefitDetail?.Any(e => e.BenefitDetailId == id)).GetValueOrDefault();
        }
    }
}
