using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narzedzia.Models
{
    public class Awaria
    {

        [Key]
        [Display(Name = "ID Awarii:")]
        public int IdAwaria { get; set; }

        [Required(ErrorMessage = "Wybierz narzędzie którego dotyczy awaria")]
        [Display(Name = "Nazwa narzędzia:")]
        public int NarzedzieId { get; set; }

        [Required(ErrorMessage = "Opis awarii jest wymagany.")]
        [StringLength(255, MinimumLength = 20, ErrorMessage = "Opis awarii musi się składać z co najmniej 20 znaków")]
        [Display(Name = "Opis Awarii:")]
        public string DescriptionAwaria { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Numer telefonu musi składać się z dokładnie 9 cyfr.")]
        [CustomPhoneNumber(ErrorMessage = "Podany numer telefonu jest nieprawidłowy.")]
        [Display(Name = "Numer telefonu do kontaktu:")]
        public string NumberAwaria { get; set; }



        [Required]
        [Display(Name = "Data zgłoszenia:")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowy format daty.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DataPrzyjecia { get; set; }

        [Display(Name = "Użytkownik zgłaszający:")]
        public string? UzytkownikId { get; set; }

        [Required]
        [Display(Name = "Status zgłoszenia:")]
        [DefaultValue(0)]
        public StatusAwaria Status { get; set; }

        [Display(Name = "Notatka techniczna:")]
        public string? NotatkaTechniczna { get; set; }

        [ForeignKey("UzytkownikId")]
        public virtual Uzytkownik? Uzytkownicy { get; set; }

        [ForeignKey("NarzedzieId")]
        public virtual Narzedzie? Narzedzie { get; set; }

        [Display(Name = "Użytkownik realizujący:")]
        public string? UzytkownikRealizujacyId { get; set; }
        [ForeignKey("UzytkownikRealizujacyId")]
        public virtual Uzytkownik? UzytkownikRealizujacy { get; set; }
        [Display(Name = "Stanowisko użytkownika:")]
        public int? StanowiskoId { get; set; }

        [ForeignKey("StanowiskoId")]
        public virtual Stanowisko? Stanowisko { get; set; }

        [Display(Name = "Wydział:")]
        public int? WydzialId { get; set; }

        [ForeignKey("WydzialId")]
        public virtual Wydzial? Wydzial { get; set; }






    }
    public enum StatusAwaria
    {
        nowe,
        realizacja,
        zakończone,
        oczekujące

    }
}
