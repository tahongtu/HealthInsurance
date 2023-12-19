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
    public class UserInformationsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public UserInformationsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: UserInformations
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.UserInformation.Include(u => u.User);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: UserInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserInformation == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.InfId == id);
            if (userInformation == null)
            {
                return NotFound();
            }

            return View(userInformation);
        }

        // GET: UserInformations/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password");
            return View();
        }

        // POST: UserInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InfId,AvtImage,FullName,Birthday,Sex,CardId,LicenseDate,FrontImage,BackImage,PhoneNumber,Address,UserId")] UserInformation userInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", userInformation.UserId);
            return View(userInformation);
        }

        // GET: UserInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserInformation == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation.FindAsync(id);
            if (userInformation == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", userInformation.UserId);
            return View(userInformation);
        }

        // POST: UserInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InfId,AvtImage,FullName,Birthday,Sex,CardId,LicenseDate,FrontImage,BackImage,PhoneNumber,Address,UserId")] UserInformation userInformation)
        {
            if (id != userInformation.InfId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInformationExists(userInformation.InfId))
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
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", userInformation.UserId);
            return View(userInformation);
        }

        // GET: UserInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserInformation == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.InfId == id);
            if (userInformation == null)
            {
                return NotFound();
            }

            return View(userInformation);
        }

        // POST: UserInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserInformation == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.UserInformation'  is null.");
            }
            var userInformation = await _context.UserInformation.FindAsync(id);
            if (userInformation != null)
            {
                _context.UserInformation.Remove(userInformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInformationExists(int id)
        {
          return (_context.UserInformation?.Any(e => e.InfId == id)).GetValueOrDefault();
        }
    }
}
