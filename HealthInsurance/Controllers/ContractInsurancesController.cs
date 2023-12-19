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
    public class ContractInsurancesController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public ContractInsurancesController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: ContractInsurances
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.ContractInsurance.Include(c => c.InsuranceProducts).Include(c => c.User);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: ContractInsurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContractInsurance == null)
            {
                return NotFound();
            }

            var contractInsurance = await _context.ContractInsurance
                .Include(c => c.InsuranceProducts)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contractInsurance == null)
            {
                return NotFound();
            }

            return View(contractInsurance);
        }

        // GET: ContractInsurances/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password");
            return View();
        }

        // POST: ContractInsurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,DateStart,DateEnd,ProductId,UserId")] ContractInsurance contractInsurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractInsurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", contractInsurance.ProductId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", contractInsurance.UserId);
            return View(contractInsurance);
        }

        // GET: ContractInsurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContractInsurance == null)
            {
                return NotFound();
            }

            var contractInsurance = await _context.ContractInsurance.FindAsync(id);
            if (contractInsurance == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", contractInsurance.ProductId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", contractInsurance.UserId);
            return View(contractInsurance);
        }

        // POST: ContractInsurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,DateStart,DateEnd,ProductId,UserId")] ContractInsurance contractInsurance)
        {
            if (id != contractInsurance.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractInsurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractInsuranceExists(contractInsurance.ContractId))
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
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", contractInsurance.ProductId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", contractInsurance.UserId);
            return View(contractInsurance);
        }

        // GET: ContractInsurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContractInsurance == null)
            {
                return NotFound();
            }

            var contractInsurance = await _context.ContractInsurance
                .Include(c => c.InsuranceProducts)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contractInsurance == null)
            {
                return NotFound();
            }

            return View(contractInsurance);
        }

        // POST: ContractInsurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContractInsurance == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.ContractInsurance'  is null.");
            }
            var contractInsurance = await _context.ContractInsurance.FindAsync(id);
            if (contractInsurance != null)
            {
                _context.ContractInsurance.Remove(contractInsurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractInsuranceExists(int id)
        {
          return (_context.ContractInsurance?.Any(e => e.ContractId == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> Contract(int prdId, int packageId)
        {
            

            var insuranceProducts = await _context.InsuranceProducts
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ProductId == prdId);
            
            if (insuranceProducts == null)
            {
                return NotFound();
            }
            var benefitDetail = _context.BenefitDetail.Where(b => b.ProductId == prdId).ToList();
            var diseaseList = _context.DiseaseList.Where(d => d.ProductId == prdId).ToList();
            var outstandingBenefit = _context.OutstandingBenefit.Where(o => o.ProductId == prdId).ToList();
            var participationInfos = _context.ParticipationInformation.Where(p => p.ProductId == prdId).ToList();
            var packageInsurance = await _context.PackageInsurance.Include(n => n.InsuranceProducts).FirstOrDefaultAsync(n => n.PackageId == packageId);
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId);
            var userInfos = await _context.UserInformation.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId);

            var viewModel = new ContractInformationModel
            {
                insuranceProducts = insuranceProducts,
                benefitDetails = benefitDetail,
                diseaseLists = diseaseList,
                outstandingBenefits = outstandingBenefit,
                participationInformation = participationInfos,
                packageInsurances = packageInsurance,
                user = user,
                userInformation = userInfos

            };
            return View(viewModel);
        }


    }
}
