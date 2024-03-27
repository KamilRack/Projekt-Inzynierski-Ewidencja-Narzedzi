using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Narzedzia.Models
{
    public class Stanowisko
    {
        [Key]
        [Display(Name = "Identyfikator stanowiska:")]
        public int StanowiskoId { get; set; }

        [Required]
        [Display(Name = "Nazwa stanowiska:")]
        [MaxLength(3)]
        public string NazwaStanowiska { get; set; }

        [Required]
        [Display(Name = "Czy stanowisko aktywne")]
        [DefaultValue(true)]
        public bool Active { get; set; }

        public virtual List<Uzytkownik>? Uzytkownicy { get; set; }
        public virtual List<Awaria>? Awarie { get; set; }

    }
}
