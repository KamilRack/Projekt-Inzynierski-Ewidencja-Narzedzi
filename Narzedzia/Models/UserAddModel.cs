using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Narzedzia.Models
{
    public class UserAddModel
    {
        [Display(Name = "Imię użytkownika:")]
        [MaxLength(20)]
        public string? Imie { get; set; }

        [Display(Name = "Nazwisko użytkownika:")]
        [MaxLength(50)]
        public string? Nazwisko { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Numer kontrolny:")]
        [MaxLength(5)]
        public int NrKontrolny { get; set; }

        [Display(Name = "Wydzial:")]
        public int WydzialId { get; set; }

        [Display(Name = "Stanowisko:")]
        public int StanowiskoId { get; set; }
    }
}
