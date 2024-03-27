namespace Narzedzia.Helpers
{
    public class JSONListHelper
    {
        public static string GetEventListJSONString(List<Models.Events> events)
        {
            var eventlist = new List<Event>();
            foreach (var model in events)
            {
                var myevent = new Event()
                {
                    id = model.IdCal,
                    start = model.StartCal,
                    end = model.EndCal,
                    description = model.DescriptionCal,
                    title = model.NameCal
                };

                myevent.resourceIds["stanowisko"] = model.Stanowisko.StanowiskoId;
                myevent.resourceIds["wydzial"] = model.Wydzial.WydzialId;
                myevent.resourceIds["narzedzie"] = model.Narzedzie.NarzedzieId;

                eventlist.Add(myevent);
            }
            return System.Text.Json.JsonSerializer.Serialize(eventlist);
        }
        public static string GetResourceListJSONString(List<Models.Stanowisko> stanowiskos, List<Models.Wydzial> wydzials, List<Models.Narzedzie> narzedzies)
        {
            var resourcelist = new List<Resource>();

            foreach (var sta in stanowiskos)
            {
                var resource = new Resource()
                {
                    id = sta.StanowiskoId,
                    title = sta.NazwaStanowiska,
                };

                resourcelist.Add(resource);
            }
            foreach (var wyd in wydzials)
            {
                var resource = new Resource()
                {
                    id = wyd.WydzialId,
                    title = wyd.NazwaWydzialu,
                };

                resourcelist.Add(resource);
            }
            foreach (var nar in narzedzies)
            {
                var resource = new Resource()
                {
                    id = nar.NarzedzieId,
                    title = nar.Nazwa,
                };

                resourcelist.Add(resource);
            }
            return System.Text.Json.JsonSerializer.Serialize(resourcelist);
        }

        public class Event
        {
            public int id { get; set; }
            public string title { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public Dictionary<string, int> resourceIds { get; set; } = new Dictionary<string, int>();
            public string description { get; set; }
        }

        public class Resource
        {
            public int id { get; set; }
            public string title { get; set; }

        }
    }
}

