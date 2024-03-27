using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Narzedzia.Models
{
    public class Narzedzie
    {
        [Key]
        [Display(Name = "Identyfikator narzędzia:")]
        public int NarzedzieId { get; set; }

        [Display(Name = "Producent narzędzia:")]
        public int ProducentId { get; set; }

        [Display(Name = "Kategoria narzędzia:")]
        public int KategoriaId { get; set; }

        [Required]
        [Display(Name = "Data przyjęcia:")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowy format daty.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DataPrzyjecia { get; set; }

        [Display(Name = "Użytkownik narzędzia:")]
        public string? UzytkownikId { get; set; }

        [Display(Name = "Numer narzędzia:")]
        public int NumerNarzedzia { get; set; }

        [MaxLength(40)]
        [Display(Name = "Nazwa narzędzia:")]
        public string Nazwa { get; set; }

        [Required]
        [Display(Name = "Status narzędzia")]
        [DefaultValue(0)]
        public Status Status { get; set; }


        [ForeignKey("UzytkownikId")]
        public virtual Uzytkownik? Uzytkownicy { get; set; }

        [ForeignKey("ProducentId")]
        public virtual Producent? Producenci { get; set; }

        [ForeignKey("KategoriaId")]
        public virtual Kategoria? Kategorie { get; set; }

        public virtual List<Awaria>? Awarie { get; set; }

        [Display(Name = "Zdjęcie narzędzia")]
        public string? ZdjecieFileName { get; set; } // Przechowuje nazwę pliku zdjęcia

        [Display(Name = "Uwagi")]
        public string? Uwagi { get; set; }

        [Display(Name = "Usunąć zdjęcie")]
        public bool UsunZdjecie { get; set; }



    }

    public enum Status
    {
        przyjęte,
        używane,
        zlikwidowane,
        naprawiane
    }
}
