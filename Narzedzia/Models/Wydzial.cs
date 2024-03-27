using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Narzedzia.Models
{
    public class Wydzial
    {
        [Key]
        [Display(Name = "Identyfikator wydziału:")]
        public int WydzialId { get; set; }

        [Required]
        [Display(Name = "Nazwa wydziału:")]
        [MaxLength(3)]
        public string NazwaWydzialu { get; set; }

        [Required]
        [Display(Name = "Czy wydział aktywny")]
        [DefaultValue(true)]
        public bool Active { get; set; }

        public virtual List<Uzytkownik>? Uzytkownicy { get; set; }
        public virtual List<Awaria>? Awarie { get; set; }
        public virtual ICollection<Events> Events { get; set; }


    }
}
