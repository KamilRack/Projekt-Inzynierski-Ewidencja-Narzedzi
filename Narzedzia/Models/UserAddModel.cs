using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Narzedzia.Models
{
    public class UserAddModel
    {
        [Required(ErrorMessage = "Pole Imię użytkownika jest wymagane.")]
        [Display(Name = "Imię użytkownika:")]
        [MaxLength(20)]
        public string? Imie { get; set; }

        [Required(ErrorMessage = "Pole Nazwisko użytkownika jest wymagane.")]
        [Display(Name = "Nazwisko użytkownika:")]
        [MaxLength(50)]
        public string? Nazwisko { get; set; }

        [Required(ErrorMessage = "Pole Email jest wymagane.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Numer kontrolny jest wymagane.")]

        [Display(Name = "Numer kontrolny:")]
        [MaxLength(5)]
        public int NrKontrolny { get; set; }

        [Display(Name = "Wydzial:")]
        public int WydzialId { get; set; }

        [Display(Name = "Stanowisko:")]
        public int StanowiskoId { get; set; }
    }
}
