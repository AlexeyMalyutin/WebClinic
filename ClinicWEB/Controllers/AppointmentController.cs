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
    public class AppointmentController : Controller
    {
        private readonly ClinicContext _context;

        public AppointmentController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient);
            return View(await clinicContext.ToListAsync());
        }

       

        // GET: Appointment/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName");
            return View();
        }

        public JsonResult GetPatients(int id)
        {
            var tempPatients = from a in _context.Patients
                           join b in _context.Registrations
                           on a.Id equals b.PatientId
                           select new
                           {
                               Id = a.Id,
                               FullName = a.FullName,
                               DoctorId = b.DoctorId

                           };
            
            var patients = tempPatients.Where(x => x.DoctorId == id).ToList();

            return Json(new SelectList(patients, "Id", "FullName"));
        }

        public JsonResult GetDate(int PatientId, int DoctorId)
        {
            var date = _context.Registrations
                .Where(x => x.DoctorId == DoctorId)
                .Where(y => y.PatientId == PatientId)
                .Select(i => i.DateTime);
            //var date = _context.Registrations.Where(i => i.PatientId == id).FirstOrDefault();
            return Json(date);
        }

        public JsonResult GetSpecializations(int DoctorId)
        {
            var specializations = _context.Doctors.
                Where(x => x.Id == DoctorId).
                Select(y => y.Specialization);

            return Json(specializations);
        }
        // POST: Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var record = _context.Registrations.
                    Where(i => i.PatientId == appointment.PatientId && i.DoctorId == appointment.DoctorId).
                    FirstOrDefault();

                _context.Registrations.Remove(record);
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Id", appointment.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", appointment.PatientId);
            return View(appointment);
        }

        
        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }

        public JsonResult GetWorkload(int DoctorId)
        {
            var date1 = DateTime.Now.AddMonths(-1);
            var date2 = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
            var count = _context.Appointments.Where(x => x.DoctorId == DoctorId)
                .Count();
            return Json(count);
        }

        public IActionResult Epidemic()
        {
            ViewData["Epidemics"] = new SelectList(_context.Appointments, "Diagnosis", "Diagnosis");
            return View();
        }

        public JsonResult GetIllCount(string diagnosis)
        {
            var count = _context.Appointments.Where(i => i.Diagnosis == diagnosis).Count();
            return Json(count);
        }
    }
}
