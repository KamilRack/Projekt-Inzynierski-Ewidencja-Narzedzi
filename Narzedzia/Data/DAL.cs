using Microsoft.EntityFrameworkCore;
using Narzedzia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Narzedzia.Data
{
    public interface IDAL
    {

    public List<Events> GetEvents();
    public List<Events> GetMyEvents();

    public Events GetEvent(int id);
    public void CreateEvent(IFormCollection form);
    public void UpdateEvent(IFormCollection form);
    public void DeleteEvent(int id);

    public List<Wydzial> GetWydzials();
    public Wydzial GetWydzial(int id);
    public List<Stanowisko> GetStanowiskos();
    public Stanowisko GetStanowisko(int id);

    public List<Narzedzie> GetNarzedzias();
    public Narzedzie GetNarzedzia(int id);


    }
    public class DAL : IDAL
    {
        private readonly ApplicationDbContext _context;

        public DAL(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<Events> GetEvents()
        {
            return _context.Events.Include(e => e.Wydzial).Include(e => e.Narzedzie).Include(e => e.Stanowisko).ToList();
        }
        public List<Events> GetMyEvents()
        {
            return _context.Events.ToList();
        }

       
        public Events GetEvent(int id)
        {
            /*            return db.Events.FirstOrDefault(x => x.IdCal == id);
            */
            return _context.Events
                 .Include(e => e.Stanowisko)
                 .Include(e => e.Wydzial)
                 .Include(e => e.Narzedzie)
                 .Include(e => e.Narzedzie.Uzytkownicy)
                 .FirstOrDefault(x => x.IdCal == id);
        }
        public void CreateEvent(IFormCollection form)
        {
            
                var newEvent = new Events();

                newEvent.NameCal = form["Events.NameCal"].ToString();
                newEvent.DescriptionCal = form["Events.DescriptionCal"].ToString();
                newEvent.StartCal = DateTime.Parse(form["Events.StartCal"].ToString());
                newEvent.EndCal = DateTime.Parse(form["Events.EndCal"].ToString());

                var narzedziename = form["Narzedzia"].ToString();
                var stanowiskaname = form["Stanowiska"].ToString();
                var wydzialyname = form["Wydzialy"].ToString();

                newEvent.NarzedzieId = _context.Narzedzia.FirstOrDefault(x => x.Nazwa == narzedziename)?.NarzedzieId;
                newEvent.StanowiskoId = _context.Stanowiska.FirstOrDefault(x => x.NazwaStanowiska == stanowiskaname)?.StanowiskoId;
                newEvent.WydzialId = _context.Wydzialy.FirstOrDefault(x => x.NazwaWydzialu == wydzialyname)?.WydzialId;

            _context.Events.Add(newEvent);
            _context.SaveChanges();

            
           
        }

        public void UpdateEvent(IFormCollection form)
        {
            var narzedziename = form["Narzedzia"].ToString();
            var stanowiskaname = form["Stanowiska"].ToString();
            var wydzialyname = form["Wydzialy"].ToString();
            var eventid = int.Parse(form["Events.IdCal"]);
           // var eventid = form["IdCal"].ToString();
            var myevent = _context.Events.FirstOrDefault(x => x.IdCal == eventid );
            var narzedzia = _context.Narzedzia.FirstOrDefault(x => x.Nazwa == narzedziename);
            var stanowiska = _context.Stanowiska.FirstOrDefault(x => x.NazwaStanowiska == stanowiskaname);
            var wydzialy = _context.Wydzialy.FirstOrDefault(x => x.NazwaWydzialu == wydzialyname);
            myevent.UpdateEvent(form, narzedzia, stanowiska, wydzialy);
            _context.Entry(myevent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteEvent(int id)
        {
            var myevent = _context.Events.Find(id);
            _context.Events.Remove(myevent);
            _context.SaveChanges();
        }

        public List<Wydzial> GetWydzials()
        {
            return _context.Wydzialy.ToList();
        }
        public Wydzial GetWydzial(int id)
        {
            return _context.Wydzialy.Find(id);
        }
        public List<Stanowisko> GetStanowiskos()
        {
            return _context.Stanowiska.ToList();

        }
        public Stanowisko GetStanowisko(int id)
        {
            return _context.Stanowiska.Find(id);

        }
        public List<Narzedzie> GetNarzedzias()
        {
            return _context.Narzedzia.ToList();

        }
        public Narzedzie GetNarzedzia(int id)
        {
            return _context.Narzedzia.Find(id);

        }
    }

}
