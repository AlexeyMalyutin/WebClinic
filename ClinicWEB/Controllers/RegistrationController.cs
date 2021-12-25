using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicWEB.Models;
using System.Text.RegularExpressions;

namespace ClinicWEB.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ClinicContext _context;

        public RegistrationController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Registration
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.Registrations.Include(r => r.Doctor).Include(r => r.Patient);
            return View(await clinicContext.ToListAsync());
        }

        // GET: Registration/Create
        public IActionResult Create(bool isFailed=false)
        {
            #region
            /*var specializations = _context.Doctors.Select(i => i.Specialization).Distinct().ToList();
            ViewBag.Specialization = specializations;

            var patients = _context.Patients.Select(i => i.FullName).ToList();
            ViewBag.PatientFullName = patients;

            var doctors = _context.Doctors.Select(i => i.FullName).ToList();
            ViewBag.DoctorFullName = doctors;*/
            #endregion
            ViewBag.IsFailed = isFailed;

            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName");
            ViewData["Specializations"] = new SelectList(_context.Doctors.Select(i=>i.Specialization).Distinct().ToList());

            return View();
        }

        public JsonResult GetDoctors(string specialization)
        {
            var doctors = _context.Doctors.Where(d => d.Specialization == specialization).ToList();
            return Json(new SelectList(doctors, "Id", "FullName"));
        }

        // POST: Registration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registration registration)
        {
            if (ModelState.IsValid)
            {
                var flag = _context.Registrations.Any(i => i.PatientId == registration.PatientId && i.DoctorId == registration.DoctorId);
                if (flag)
                {
                    return RedirectToAction("Create", new { isFailed = true });
                }
                else
                {
                    _context.Add(registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                
            }
            
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Id", registration.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", registration.PatientId);
            return View(registration);
        }

        
        // GET: Registration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Doctor)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.Id == id);
        }

        public IActionResult Queue()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName");
            ViewData["Specializations"] = new SelectList(_context.Doctors.Select(i => i.Specialization).Distinct().ToList());

            return View();
        }

        public JsonResult GetQueueCount(int DoctorId)
        {
            //DateTime prevMonth = DateTime.Now.AddMonths(-1);
            var count = _context.Registrations.Where(i => i.DoctorId == DoctorId).Count();
            return Json(count);
        }

    }
}
