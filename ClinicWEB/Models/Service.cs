using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicWEB.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Display(Name = "Услуга")]
        [Required(ErrorMessage = "Заполните поле")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Заполните поле")]
        public decimal Price { get; set; }

        public ICollection<Sell> Sells { get; set; }
    }
}
