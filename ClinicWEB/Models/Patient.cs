using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicWEB.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Display(Name = "ФИО пациента")]
        [Required(ErrorMessage = "Заполните поле")]
        public string FullName { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Заполните поле")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        
        public string Birthday { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Registration> Registrations { get; set; }

        public ICollection<Sell> Sells { get; set; }
    }
}
