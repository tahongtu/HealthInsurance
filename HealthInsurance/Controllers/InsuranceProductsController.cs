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
    public class InsuranceProductsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public InsuranceProductsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: InsuranceProducts
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.InsuranceProducts.Include(i => i.Category);
            return View(await healthInsuranceContext.ToListAsync());
        }

        public async Task<IActionResult> getAllProduct()
        {
            var healthInsuranceContext = _context.InsuranceProducts.Include(i => i.Category);
            return View(await healthInsuranceContext.ToListAsync());
        }

        public async Task<IActionResult> ProductByCategory(int catId)
        {
            var healthInsuranceContext = _context.InsuranceProducts.Include(i => i.Category).Where(i => i.CategoryId == catId);
            return View(await healthInsuranceContext.ToListAsync());
        }


        // GET: InsuranceProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsuranceProducts == null)
            {
                return NotFound();
            }

            var insuranceProducts = await _context.InsuranceProducts
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (insuranceProducts == null)
            {
                return NotFound();
            }

            return View(insuranceProducts);
        }

        // GET: InsuranceProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: InsuranceProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Overview,CategoryId")] InsuranceProducts insuranceProducts)
        {
            if (insuranceProducts.ProductName != null && insuranceProducts.Description != null 
                && insuranceProducts.Overview != null)
            {
                _context.Add(insuranceProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                _context.Add(insuranceProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", insuranceProducts.CategoryId);
            return View(insuranceProducts);
        }

        // GET: InsuranceProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsuranceProducts == null)
            {
                return NotFound();
            }

            var insuranceProducts = await _context.InsuranceProducts.FindAsync(id);
            if (insuranceProducts == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryDesription", insuranceProducts.CategoryId);
            return View(insuranceProducts);
        }

        // POST: InsuranceProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Overview,CategoryId")] InsuranceProducts insuranceProducts)
        {
            if (id != insuranceProducts.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceProductsExists(insuranceProducts.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryDesription", insuranceProducts.CategoryId);
            return View(insuranceProducts);
        }

        // GET: InsuranceProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsuranceProducts == null)
            {
                return NotFound();
            }

            var insuranceProducts = await _context.InsuranceProducts
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (insuranceProducts == null)
            {
                return NotFound();
            }

            return View(insuranceProducts);
        }

        // POST: InsuranceProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsuranceProducts == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.InsuranceProducts'  is null.");
            }
            var insuranceProducts = await _context.InsuranceProducts.FindAsync(id);
            if (insuranceProducts != null)
            {
                _context.InsuranceProducts.Remove(insuranceProducts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceProductsExists(int id)
        {
          return (_context.InsuranceProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }



        [HttpGet]
        public async Task<IActionResult> getProductDetail(int prdid)
        {

            if (prdid == null || _context.InsuranceProducts == null)
            {
                return NotFound();
            }

            var insuranceProducts = await _context.InsuranceProducts
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ProductId == prdid);
            if (insuranceProducts == null)
            {
                return NotFound();
            }
            var benefitDetail = _context.BenefitDetail.Where(b => b.ProductId == prdid).ToList();
            var diseaseList = _context.DiseaseList.Where(d => d.ProductId == prdid).ToList();
            var outstandingBenefit = _context.OutstandingBenefit.Where(o => o.ProductId == prdid).ToList();
            var participationInfos = _context.ParticipationInformation.Where(p => p.ProductId == prdid).ToList();
            var packageInsurance = _context.PackageInsurance.Where(a => a.ProductId == prdid).ToList();

            var viewModel = new ProductDetailViewModel
            {
                insuranceProducts = insuranceProducts,
                benefitDetails = benefitDetail,
                diseaseLists = diseaseList,
                outstandingBenefits = outstandingBenefit,
                participationInformation = participationInfos,
                packageInsurances = packageInsurance
                
            };


            return View(viewModel);
        }

        

    }
}
