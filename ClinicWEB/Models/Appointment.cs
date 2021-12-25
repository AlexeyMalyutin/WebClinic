using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWEB.Models
{
    public class Appointment
    {

        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        [Display(Name = "Диагноз")]
        [Required(ErrorMessage = "Заполните поле")]
        public string Diagnosis { get; set; }

        [Display(Name = "Дата приёма")]
        [Required(ErrorMessage = "Заполните поле")]

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string DateTime { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
