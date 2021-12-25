using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicWEB.Models;
using Newtonsoft.Json;

namespace ClinicWEB.Controllers
{
    public class SellController : Controller
    {
        private readonly ClinicContext _context;

        public SellController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Sells
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.Sells.Include(s => s.Patient).Include(s => s.Service);
            return View(await clinicContext.ToListAsync());
        }

        // GET: Sells/Create
        public IActionResult Create()
        {
            var servicesWithPrices = _context.Services.Select(str => new
            {
                Id = str.Id,
                Price = str.Price,
                Name = str.Name + " - " + str.Price.ToString("c")
            }).ToList();

            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName");
            ViewData["ServiceId"] = new SelectList(servicesWithPrices, "Id", "Name");
            return View();
        }

        public JsonResult GetPrice(int ServiceId)
        {
            var price = _context.Services.Where(i => i.Id == ServiceId).Select(p => p.Price).FirstOrDefault();
            return Json(price);
        }

        // POST: Sells/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,ServiceId,Number,Sum")] Sell sell)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sell);
                await _context.SaveChangesAsync();
                TempData["check"] = sell.Id;
                return RedirectToAction("Check", "Sell");
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName", sell.PatientId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", sell.ServiceId);
            return View(sell);
        }

        public IActionResult Check()
        {
            var SellId = (int)TempData["check"];
            var sell = _context.Sells.Include(x => x.Patient).Include(y => y.Service).Where(i => i.Id == SellId).FirstOrDefault();
            
            return View(sell);
        }

        // GET: Sells/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sell = await _context.Sells
                .Include(s => s.Patient)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sell == null)
            {
                return NotFound();
            }

            return View(sell);
        }

        // POST: Sells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sell = await _context.Sells.FindAsync(id);
            _context.Sells.Remove(sell);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellExists(int id)
        {
            return _context.Sells.Any(e => e.Id == id);
        }
    }
}
