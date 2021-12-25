using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWEB.Models
{
    public class ClinicContext: DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
               
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Service> Services { get; set; }

    }
}
