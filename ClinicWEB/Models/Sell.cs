using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWEB.Models
{
    public class Sell
    {
        public int Id { get; set; }

        [Display(Name = "ФИО пациента")]
        [Required(ErrorMessage = "Заполните поле")]
        public int PatientId { get; set; }

        [Display(Name = "услуга")]
        [Required(ErrorMessage = "Заполните поле")]
        public int ServiceId { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Заполните поле")]
        public int Number { get; set; }

        [Display(Name = "Итого")]
        [Required(ErrorMessage = "Заполните поле")]
        public int Sum { get; set; }

        public Patient Patient { get; set; }

        public Service Service { get; set; }
    }
}
