using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narzedzia.Models
{

    public class Events
    {

        [Key]
        [Display(Name = "Id:")] 
        public int IdCal { get; set; }
        [Required(ErrorMessage = "Pole Temat jest wymagane.")]
        [Display(Name = "Temat:")]
        public string NameCal { get; set; }
        [Required(ErrorMessage = "Pole Opis jest wymagane.")]
        [Display(Name = "Opis:")]
        public string DescriptionCal { get; set; }
        [Display(Name = "Data od:")]
        [Required(ErrorMessage = "Pole Data od jest wymagane.")]
        public DateTime StartCal { get; set; }
        [Required(ErrorMessage = "Pole Data do jest wymagane.")]
        [Display(Name = "Data do:")]
        public DateTime EndCal { get; set; }

        //Relational data
        [ForeignKey("Narzedzie")]
        public int? NarzedzieId { get; set; }
        public virtual Narzedzie Narzedzie { get; set; }

        [ForeignKey("Stanowisko")]
        public int? StanowiskoId { get; set; }
        public virtual Stanowisko Stanowisko { get; set; }

        [Display(Name = "Wydział:")]

        public int? WydzialId { get; set; }
        [ForeignKey("WydzialId")]

        public virtual Wydzial Wydzial { get; set; }

        public Events(IFormCollection form, Narzedzie narzedzia, Stanowisko stanowiska, Wydzial wydzialy)
        {
            /*            IdCal = int.Parse(form["Events.IdCal"].ToString());
            */
            NameCal = form["Events.NameCal"].ToString();
            DescriptionCal = form["Events.DescriptionCal"].ToString();
            StartCal = DateTime.Parse(form["Events.StartCal"].ToString());
            EndCal = DateTime.Parse(form["Events.EndCal"].ToString());
            Narzedzie = narzedzia; // Używam właściwości NarzedzieId, a nie Narzedzie
            Stanowisko = stanowiska; // Używam właściwości StanowiskoId, a nie Stanowisko
            Wydzial = wydzialy; // Używam właściwości WydzialId, a nie Wydzial
        }

        public void UpdateEvent(IFormCollection form, Narzedzie narzedzia, Stanowisko stanowiska, Wydzial wydzialy)
        {
/*            IdCal = int.Parse(form["Events.IdCal"].ToString());
*/            
            NameCal = form["Events.NameCal"].ToString();
            DescriptionCal = form["Events.DescriptionCal"].ToString();
            StartCal = DateTime.Parse(form["Events.StartCal"].ToString());
            EndCal = DateTime.Parse(form["Events.EndCal"].ToString());
            Narzedzie = narzedzia;
            Stanowisko = stanowiska;
            Wydzial = wydzialy;
        }
        public Events(IFormCollection form)
        {

        }
        public Events()
        {
        }

    }
}
