using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Narzedzia.Models
{
    public class Producent
    {
        [Key]
        [Display(Name = "Identyfikator producenta:")]
        public int ProducentId { get; set; }

        [Required]
        [Display(Name = "Nazwa producenta:")]
        [MaxLength(40)]
        public string NazwaProducenta { get; set; }

        [Required]
        [Display(Name = "Czy producent aktywny")]
        [DefaultValue(true)]
        public bool Active { get; set; }

        public virtual List<Narzedzie>? Narzedzia { get; set; }
    }
}
