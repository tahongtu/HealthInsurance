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
    public class ConsultMessagesController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public ConsultMessagesController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: ConsultMessages
        public async Task<IActionResult> Index()
        {
              return _context.ConsultMessage != null ? 
                          View(await _context.ConsultMessage.ToListAsync()) :
                          Problem("Entity set 'HealthInsuranceContext.ConsultMessage'  is null.");
        }

        // GET: ConsultMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConsultMessage == null)
            {
                return NotFound();
            }

            var consultMessage = await _context.ConsultMessage
                .FirstOrDefaultAsync(m => m.ConsultId == id);
            if (consultMessage == null)
            {
                return NotFound();
            }

            return View(consultMessage);
        }

        // GET: ConsultMessages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConsultMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultId,ClientName,PhoneNumber,InsuranceName,Email,Message")] ConsultMessage consultMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultMessage);
        }

        // GET: ConsultMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConsultMessage == null)
            {
                return NotFound();
            }

            var consultMessage = await _context.ConsultMessage.FindAsync(id);
            if (consultMessage == null)
            {
                return NotFound();
            }
            return View(consultMessage);
        }

        // POST: ConsultMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultId,ClientName,PhoneNumber,InsuranceName,Email,Message")] ConsultMessage consultMessage)
        {
            if (id != consultMessage.ConsultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultMessageExists(consultMessage.ConsultId))
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
            return View(consultMessage);
        }

        // GET: ConsultMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConsultMessage == null)
            {
                return NotFound();
            }

            var consultMessage = await _context.ConsultMessage
                .FirstOrDefaultAsync(m => m.ConsultId == id);
            if (consultMessage == null)
            {
                return NotFound();
            }

            return View(consultMessage);
        }

        // POST: ConsultMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConsultMessage == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.ConsultMessage'  is null.");
            }
            var consultMessage = await _context.ConsultMessage.FindAsync(id);
            if (consultMessage != null)
            {
                _context.ConsultMessage.Remove(consultMessage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultMessageExists(int id)
        {
          return (_context.ConsultMessage?.Any(e => e.ConsultId == id)).GetValueOrDefault();
        }
    }
}
