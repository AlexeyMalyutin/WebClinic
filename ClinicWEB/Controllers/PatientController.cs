using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicWEB.Models;

namespace ClinicWEB.Controllers
{
    public class PatientController : Controller
    {
        private readonly ClinicContext _context;

        public PatientController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Patient
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patients.OrderBy(i=>i.Birthday).ToListAsync());
        }

        // GET: Patient/Create
        public IActionResult Create(bool isFailed = false)
        {
            ViewBag.IsFailed = isFailed;
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Birthday")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                var flag = _context.Patients.Any(i => i.FullName == patient.FullName && i.Birthday == patient.Birthday);
                if(flag)
                {
                    return RedirectToAction("Create", new { isFailed = true });
                }
                ViewBag.Incorrect = null;
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(patient);
        }

        
        // GET: Patient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
