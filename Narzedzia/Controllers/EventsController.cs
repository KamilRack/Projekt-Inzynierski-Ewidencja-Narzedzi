using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Narzedzia.Data;
using Narzedzia.Models;
using Narzedzia.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Narzedzia.Controllers;
using OfficeOpenXml;
using Microsoft.AspNet.SignalR;

namespace Narzedzia.Controllers
{
    [Authorize(Roles = "admin, nadzor")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDAL _dal;

        public EventsController(IDAL dal, ApplicationDbContext context, ApplicationDbContext dbContext)
        {
            _dal = dal;
            _context = context;
            _dbContext = dbContext;

        }


        // Akcja eksportu danych do pliku Excel
        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var events = _context.Events
                .Include(e => e.Narzedzie)
                .Include(e => e.Stanowisko)
                .Include(e => e.Wydzial)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Events");

                // Dodaj nagłówki
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Temat";
                worksheet.Cells[1, 3].Value = "Opis";
                worksheet.Cells[1, 4].Value = "Data od";
                worksheet.Cells[1, 5].Value = "Data do";
                worksheet.Cells[1, 6].Value = "Narzędzie";
                worksheet.Cells[1, 7].Value = "Stanowisko";
                worksheet.Cells[1, 8].Value = "Wydział";

                // Dodaj dane
                for (int i = 0; i < events.Count; i++)
                {
                    var eventItem = events[i];

                    worksheet.Cells[i + 2, 1].Value = eventItem.IdCal;
                    worksheet.Cells[i + 2, 2].Value = eventItem.NameCal;
                    worksheet.Cells[i + 2, 3].Value = eventItem.DescriptionCal;
                    worksheet.Cells[i + 2, 4].Value = eventItem.StartCal.ToString("dd.MM.yyyy");
                    worksheet.Cells[i + 2, 5].Value = eventItem.EndCal.ToString("dd.MM.yyyy");
                    worksheet.Cells[i + 2, 6].Value = eventItem.Narzedzie?.Nazwa;
                    worksheet.Cells[i + 2, 7].Value = eventItem.Stanowisko?.NazwaStanowiska;
                    worksheet.Cells[i + 2, 8].Value = eventItem.Wydzial?.NazwaWydzialu;
                }

                // Zapisz plik
                var stream = new MemoryStream();
                package.SaveAs(stream);

                // Zwróć plik
                var fileName = "Wydarzenia_Kalendarz.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
        }
        // GET: Events
        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(_dal.GetEvents());
        }

       
        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @events = _dal.GetEvent((int)id);
            if (@events == null)
            {
                return NotFound();
            }

            return View(@events);
        }


        // GET: Events/Create
        public IActionResult Create()
        {

            var wydzials = _dal.GetWydzials();
            var stanowiskos = _dal.GetStanowiskos();
            var narzedzias = _dal.GetNarzedzias();
       
            return View(new EventsViewModel(wydzials, stanowiskos, narzedzias));

        }


        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventsViewModel vm, IFormCollection form)
        {

            try
            {
                _dal.CreateEvent(form);
                TempData["Alert"] = "Dodałeś nowe wydarzenie:: " + form["Event.Name"];
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "Błąd:: " + ex.Message;
                return View(vm);
            }
        }

 
        // GET: Events/Edit/5
        public IActionResult Edit(int? id)
        {

            var wydzials = _dal.GetWydzials();
            var stanowiskos = _dal.GetStanowiskos();
            var narzedzias = _dal.GetNarzedzias();
            if (id == null)
            {
                return NotFound();
            }

            var @event = _dal.GetEvent((int)id);
            if (@event == null)
            {
                return NotFound();
            }
            /*            var vm = new EventsViewModel(@event, _dal.GetWydzials(), _dal.GetStanowiskos(), _dal.GetNarzedzias());
            */

            var vm = new EventsViewModel(@event, wydzials, stanowiskos, narzedzias);
            return View(vm);


        }


        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
        
            try
                {
                    _dal.UpdateEvent(form);
                TempData["Alert"] = "Succes Modyfikacja zrobiona" + form["Events.NameCal"];
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
                {
                ViewData["Alert"] = "An Error Occured:" + ex.Message;
                var vm = new EventsViewModel(_dal.GetEvent(id), _dal.GetWydzials(), _dal.GetStanowiskos(),_dal.GetNarzedzias());
                return View(vm);

            }


        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @events = _dal.GetEvent((int)id);
            if (@events == null)
            {
                return NotFound();
            }

            return View(@events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _dal.DeleteEvent(id);
            TempData["Alert"] = "You deleted avent";
            return RedirectToAction(nameof(Index));
        }

    }
}
