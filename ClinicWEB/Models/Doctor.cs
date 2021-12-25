using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWEB.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Display(Name = "ФИО врача")]
        [Required(ErrorMessage = "Заполните поле")]
        public string FullName { get; set; }

        [Display(Name = "Специализация")]
        [Required(ErrorMessage = "Заполните поле")]
        public string Specialization { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}
