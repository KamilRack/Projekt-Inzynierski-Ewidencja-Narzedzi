using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Narzedzia.Models
{
    public class Kategoria
    {
        [Key]
        [Display(Name = "Identyfikator kategorii:")]
        public int KategoriaId { get; set; }

        [Required]
        [Display(Name = "Nazwa kategorii:")]
        [MaxLength(40)]
        public string NazwaKategorii { get; set; }

        [Required]
        [Display(Name = "Czy kategoria aktywna")]
        [DefaultValue(true)]
        public bool Active { get; set; }

        public virtual List<Narzedzie>? Narzedzia { get; set; }
    }
}
