using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Narzedzia.Data;
using Narzedzia.Models;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml;

namespace Narzedzia.Controllers
{

    public class AwarieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _dbContext;


        public AwarieController(ApplicationDbContext context, ApplicationDbContext dbContext)

        {
            _context = context;
            _dbContext = dbContext;
        }
        [Authorize(Roles = "admin,nadzor")]
        // Akcja eksportu danych do pliku Excel
        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var awarie = _context.Awarie
                .Include(a => a.Narzedzie)
                .Include(a => a.Uzytkownicy)
                .Include(a => a.UzytkownikRealizujacy)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Awarie");

                // Dodaj nagłówki
                worksheet.Cells[1, 1].Value = "ID Awarii";
                worksheet.Cells[1, 2].Value = "Nazwa Narzędzia";
                worksheet.Cells[1, 3].Value = "Opis Awarii";
                worksheet.Cells[1, 4].Value = "Numer Telefonu";
                worksheet.Cells[1, 5].Value = "Data Przyjęcia";
                worksheet.Cells[1, 6].Value = "Użytkownik Zgłaszający";
                worksheet.Cells[1, 7].Value = "Status Awarii";
                worksheet.Cells[1, 8].Value = "Notatka Techniczna";
                worksheet.Cells[1, 9].Value = "Użytkownik realizujący";


                // Dodaj dane
                for (int i = 0; i < awarie.Count; i++)
                {
                    var awaria = awarie[i];

                    worksheet.Cells[i + 2, 1].Value = awaria.IdAwaria;
                    worksheet.Cells[i + 2, 2].Value = awaria.Narzedzie?.Nazwa;
                    worksheet.Cells[i + 2, 3].Value = awaria.DescriptionAwaria;
                    worksheet.Cells[i + 2, 4].Value = awaria.NumberAwaria;
                    worksheet.Cells[i + 2, 5].Value = awaria.DataPrzyjecia.ToString("dd.MM.yyyy");
                    worksheet.Cells[i + 2, 6].Value = awaria.Uzytkownicy?.Imie_Nazwisko;
                    worksheet.Cells[i + 2, 7].Value = awaria.Status.ToString();
                    worksheet.Cells[i + 2, 8].Value = awaria.NotatkaTechniczna;
                    worksheet.Cells[i + 2, 9].Value = awaria.UzytkownikRealizujacy;

                }

                // Zapisz plik
                var stream = new MemoryStream();
                package.SaveAs(stream);

                // Zwróć plik
                var fileName = "Awarie.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
        }





        [Authorize(Roles = "admin,nadzor")]
        // GET: Awarie
        public async Task<IActionResult> Index()
        {

            var adminRoleId = await _context.Roles.Where(r => r.NormalizedName == "admin").Select(r => r.Id).FirstOrDefaultAsync();
            var nadzorRoleId = await _context.Roles.Where(r => r.NormalizedName == "nadzor").Select(r => r.Id).FirstOrDefaultAsync();

            var applicationDbContext = _context.Awarie.Include(a => a.Narzedzie).Include(a => a.Uzytkownicy).Include(a => a.UzytkownikRealizujacy);
            // var userList = await _context.Uzytkownicy
            //   .Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && (ur.RoleId == "adminRoleId" || ur.RoleId == "nadzorRoleId")))
            //  .ToListAsync();

            var userList = await _context.Users
        .Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && (ur.RoleId == adminRoleId || ur.RoleId == nadzorRoleId)))
        .ToListAsync();
            var userSelectList = new SelectList(userList, "Id", "Email");


            ViewData["UserList"] = userSelectList;


            return View(await applicationDbContext.ToListAsync());



            //  var applicationDbContext = _context.Awarie.Include(a => a.Narzedzie).Include(a => a.Uzytkownicy).Include(a => a.UzytkownikRealizujacy);
            // var userList = await _context.Uzytkownicy.ToListAsync();
            // ViewData["UserList"] = new SelectList(userList, "Id", "Email");

            //  return View(await applicationDbContext.ToListAsync());

        }

        [HttpPost]
        public IActionResult ZapiszUzytkownika(int awariaId, string selectedUserId)
        {
            try
            {
                // Pobierz rekord z bazy danych na podstawie awariaId
                var awaria = _context.Awarie.FirstOrDefault(a => a.IdAwaria == awariaId);

                if (awaria != null)
                {
                    // Zaktualizuj pole UzytkownikRealizujacyId na podstawie selectedUserId
                    awaria.UzytkownikRealizujacyId = selectedUserId;

                    // Zapisz zmiany w bazie danych
                    _context.SaveChanges();

                    return Json(new { success = true });
                }

                return Json(new { success = false, error = "Nie znaleziono rekordu o podanym identyfikatorze awarii." });
            }
            catch (Exception ex)
            {
                // Obsłuż ewentualny błąd
                return Json(new { success = false, error = ex.Message });
            }
        }


        [Authorize(Roles = "admin,nadzor,pracownik")]

        // GET: Awarie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Awarie == null)
            {
                return NotFound();
            }

            var awaria = await _context.Awarie
                .Include(a => a.Narzedzie)
                .Include(a => a.Uzytkownicy)
                .Include(a => a.UzytkownikRealizujacy)
                .Include(a => a.Stanowisko)
                .Include(a => a.Wydzial)
                .FirstOrDefaultAsync(m => m.IdAwaria == id);

            if (awaria == null)
            {
                return NotFound();
            }



            return View(awaria);
        }

        // GET: Awarie/Create
        public IActionResult Create()
        {
            // Pobierz zalogowanego użytkownika
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Pobierz stanowisko i wydział użytkownika
            var user = _context.Uzytkownicy.Include(u => u.Stanowiska).Include(u => u.Wydzialy).FirstOrDefault(u => u.Id == loggedInUserId);
            // Sprawdź, czy użytkownik ma rolę "admin" lub "nadzor"
            if (User.IsInRole("admin") || User.IsInRole("nadzor"))
            {
                // Jeśli tak, pobierz wszystkie narzędzia
                var allNarzedzia = _context.Narzedzia.ToList();

                // Utwórz listę wyboru (SelectList) na podstawie wszystkich narzędzi
                ViewData["NarzedzieId"] = new SelectList(allNarzedzia, "NarzedzieId", "Nazwa");
            }
            else
            {
                // Jeśli nie, pobierz narzędzia przypisane do zalogowanego użytkownika
                var narzedziaUzytkownika = _context.Narzedzia
                    .Where(n => n.UzytkownikId == loggedInUserId)
                    .ToList();

                // Utwórz listę wyboru (SelectList) na podstawie przypisanych narzędzi
                ViewData["NarzedzieId"] = new SelectList(narzedziaUzytkownika, "NarzedzieId", "Nazwa");
            }

            return View();
            //   ViewData["NarzedzieId"] = new SelectList(_context.Narzedzia, "NarzedzieId", "Nazwa");
            //   ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email");

        }

        // POST: Awarie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAwaria,NarzedzieId,DescriptionAwaria,NumberAwaria")] Awaria awaria)
        {
            if (ModelState.IsValid)
            {
                // Ustaw odpowiednie wartości dla zgłoszenia awarii
                awaria.DataPrzyjecia = DateTime.Now;
                awaria.UzytkownikId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                awaria.Status = StatusAwaria.nowe;

                // Pobierz stanowisko użytkownika
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Uzytkownicy.Include(u => u.Stanowiska).Include(u => u.Wydzialy).FirstOrDefault(u => u.Id == loggedInUserId);


                // Przypisz stanowisko użytkownika do zgłoszenia awarii
                awaria.StanowiskoId = user?.Stanowiska?.StanowiskoId;
                awaria.WydzialId = user?.Wydzialy?.WydzialId;
                // Dodaj zgłoszenie awarii do bazy danych
                _context.Add(awaria);
                await _context.SaveChangesAsync();

                if (User.IsInRole("pracownik"))
                {
                    // Przekieruj użytkownika do akcji "Index" kontrolera "Home"
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction(nameof(Index));
            }
            // Pobierz narzędzia w zależności od roli użytkownika
            var loggedInUserIdTools = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var narzedzia = User.IsInRole("admin") || User.IsInRole("nadzor")
                ? _context.Narzedzia.ToList()
                : _context.Narzedzia.Where(u => u.UzytkownikId == loggedInUserIdTools).ToList();

            // Utwórz listę wyboru (SelectList) na podstawie narzędzi
            ViewData["NarzedzieId"] = new SelectList(narzedzia, "NarzedzieId", "Nazwa", awaria.NarzedzieId);

            return View(awaria);
        }
        [Authorize(Roles = "admin,nadzor")]
        // GET: Awarie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Pobierz poprzednie informacje z sesji
            var previousAction = HttpContext.Session.GetString("PreviousAction");
            var previousController = HttpContext.Session.GetString("PreviousController");

            // Zapisz aktualne informacje do sesji
            HttpContext.Session.SetString("PreviousAction", "Edit");
            HttpContext.Session.SetString("PreviousController", "Awarie");

            if (id == null || _context.Awarie == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var awaria = await _context.Awarie.Include(n => n.Uzytkownicy).FirstOrDefaultAsync(m => m.IdAwaria == id);
            if (awaria == null)
            {
                return NotFound();
            }
            var stanowiskaList = await _context.Stanowiska.ToListAsync();
            ViewData["StanowiskaList"] = new SelectList(stanowiskaList, "StanowiskoId", "NazwaStanowiska");
            var wydzialyList = await _context.Wydzialy.ToListAsync();
            ViewData["WydzialyList"] = new SelectList(wydzialyList, "WydzialId", "NazwaWydzialu");

            ViewData["NarzedzieId"] = new SelectList(_context.Narzedzia, "NarzedzieId", "Nazwa", awaria.NarzedzieId);
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email", awaria.UzytkownikId);
            ViewData["UzytkownikRealizujacyId"] = new SelectList(_context.Uzytkownicy, "Id", "Email", awaria.UzytkownikRealizujacyId);
            if (awaria.UzytkownikId == null)
            {
                ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email");
            }
            else
            {
                ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Email", awaria.UzytkownikId);
                ViewData["Obecny"] = awaria.Uzytkownicy?.Email;
                ViewData["ObecnyId"] = awaria.UzytkownikId;
            }

            //
            var updatedAwaria = await _context.Awarie
       .Include(a => a.Narzedzie)
       .Include(a => a.Uzytkownicy)
       .Include(a => a.UzytkownikRealizujacy)
       .FirstOrDefaultAsync(m => m.IdAwaria == id);

            if (updatedAwaria != null)
            {
                awaria = updatedAwaria;
            }
            //
            return View(awaria);
        }
        [Authorize(Roles = "admin,nadzor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAwaria,NarzedzieId,DescriptionAwaria,NumberAwaria,DataPrzyjecia,UzytkownikId,Status,UzytkownikRealizujacyId,NotatkaTechniczna,StanowiskoId,WydzialId")] Awaria awaria, string? obecny)
        {
            ViewData["StanowiskaList"] = new SelectList(await _context.Stanowiska.ToListAsync(), "StanowiskoId", "NazwaStanowiska");
            ViewData["WydzialyList"] = new SelectList(await _context.Wydzialy.ToListAsync(), "WydzialId", "NazwaWydzialu");

            if (id != awaria.IdAwaria)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Awaria existingAwaria;

            // Sprawdź, czy użytkownik ma rolę "admin" lub "nadzor"
            if (!User.IsInRole("admin") && !User.IsInRole("nadzor"))
            {
                // Jeśli nie, sprawdź, czy użytkownik jest właścicielem zgłoszenia
                existingAwaria = await _context.Awarie
                    .Include(a => a.Uzytkownicy)
                    .FirstOrDefaultAsync(m => m.IdAwaria == id && m.UzytkownikId == userId);
            }
            else
            {
                // Jeśli użytkownik ma rolę "admin" lub "nadzor", pomiń sprawdzanie właściciela
                existingAwaria = await _context.Awarie
                    .Include(a => a.Uzytkownicy)
                    .FirstOrDefaultAsync(m => m.IdAwaria == id);
            }

            if (existingAwaria == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!(obecny == null))
                    {
                        existingAwaria.UzytkownikId = obecny;
                    }

                    // Modify other properties if needed
                    existingAwaria.NarzedzieId = awaria.NarzedzieId;
                    existingAwaria.DescriptionAwaria = awaria.DescriptionAwaria;
                    existingAwaria.NumberAwaria = awaria.NumberAwaria;
                    existingAwaria.DataPrzyjecia = awaria.DataPrzyjecia;
                    existingAwaria.Status = awaria.Status;
                    existingAwaria.UzytkownikRealizujacyId = awaria.UzytkownikRealizujacyId;
                    existingAwaria.NotatkaTechniczna = awaria.NotatkaTechniczna;
                    existingAwaria.StanowiskoId = awaria.StanowiskoId;
                    existingAwaria.WydzialId = awaria.WydzialId;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwariaExists(existingAwaria.IdAwaria))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                HttpContext.Session.SetString("PreviousAction", "Edit");
                HttpContext.Session.SetString("PreviousController", "Awarie");
                return RedirectToAction("Details", new { id = id });

                //   return RedirectToAction(nameof(Index));

            }

            ViewData["NarzedzieId"] = new SelectList(_context.Narzedzia, "NarzedzieId", "NarzedzieId", existingAwaria.NarzedzieId);
            ViewData["UzytkownikId"] = new SelectList(_context.Uzytkownicy, "Id", "Id", existingAwaria.UzytkownikId);
            ViewData["UzytkownikRealizujacyId"] = new SelectList(_context.Uzytkownicy, "Id", "Id", existingAwaria.UzytkownikRealizujacyId);

            return View(existingAwaria);
        }



        [Authorize(Roles = "admin,nadzor")]
        // GET: Awarie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Awarie == null)
            {
                return NotFound();
            }




            var awaria = await _context.Awarie
                .Include(a => a.Narzedzie)
                .Include(a => a.Uzytkownicy)
                .FirstOrDefaultAsync(m => m.IdAwaria == id);
            if (awaria == null)
            {
                return NotFound();
            }



            return View(awaria);



        }
        [Authorize(Roles = "admin,nadzor")]
        // POST: Awarie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Awarie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Awarie'  is null.");
            }
            var awaria = await _context.Awarie.FindAsync(id);
            if (awaria != null)
            {
                _context.Awarie.Remove(awaria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AwariaExists(int id)
        {
            return (_context.Awarie?.Any(e => e.IdAwaria == id)).GetValueOrDefault();
        }
    }

}
