using Microsoft.AspNetCore.Mvc.Rendering;

namespace Narzedzia.Models.ViewModels
{
    public class EventsViewModel
    {
        public Events Events { get; set; }
        public List<SelectListItem> Wydzial { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Stanowisko { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Narzedzie { get; set; } = new List<SelectListItem>();

        public string NazwaWydzialu { get; set; }
        public string NazwaStanowiska { get; set; }
        public string Nazwa { get; set; }

        public EventsViewModel()
        {
        }

        public EventsViewModel(Events myevent, List<Wydzial> wydzials, List<Stanowisko> stanowiskos, List<Narzedzie> narzedzias)
        {
            Events = myevent;
            NazwaWydzialu = myevent.Wydzial.NazwaWydzialu;
            NazwaStanowiska = myevent.Stanowisko.NazwaStanowiska;
            Nazwa = myevent.Narzedzie.Nazwa;

            // Ustaw właściwości Events.StanowiskoId, Events.NarzedzieId, Events.WydzialId
            Events.StanowiskoId = myevent.Stanowisko.StanowiskoId;
            Events.NarzedzieId = myevent.Narzedzie.NarzedzieId;
            Events.WydzialId = myevent.Wydzial.WydzialId;

            foreach (var even in wydzials)
            {
                Wydzial.Add(new SelectListItem() { Text = even.NazwaWydzialu });

            }
            foreach (var even in stanowiskos)
            {
                Stanowisko.Add(new SelectListItem() { Text = even.NazwaStanowiska });

            }
            foreach (var even in narzedzias)
            {
                Narzedzie.Add(new SelectListItem() { Text = even.Nazwa });

            }

        }

        public EventsViewModel(List<Wydzial> wydzials, List<Stanowisko> stanowiskos, List<Narzedzie> narzedzias)
        {
            foreach (var even in wydzials)
            {
                Wydzial.Add(new SelectListItem() { Text = even.NazwaWydzialu });
                

            }
            foreach (var even in stanowiskos)
            {
                Stanowisko.Add(new SelectListItem() { Text = even.NazwaStanowiska });
            }
            foreach (var even in narzedzias)
            {
                Narzedzie.Add(new SelectListItem() { Text = even.Nazwa });

            }


        }

    }
}