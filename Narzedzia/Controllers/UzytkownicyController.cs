using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Narzedzia.Data;
using Narzedzia.Models;
using OfficeOpenXml;

namespace Narzedzia.Controllers
{
    [Authorize(Roles = "admin")]
    public class UzytkownicyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Uzytkownik> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UzytkownicyController(ApplicationDbContext context, UserManager<Uzytkownik> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            var uzytkownicy = _context.Uzytkownicy
                                .Include(u => u.Wydzialy)
                                .Include(u => u.Stanowiska)
                                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Uzytkownicy");

                // Dodaj nagłówki
                worksheet.Cells[1, 1].Value = "Imię użytkownika";
                worksheet.Cells[1, 2].Value = "Nazwisko użytkownika";
                worksheet.Cells[1, 3].Value = "Numer kontrolny";
                worksheet.Cells[1, 4].Value = "Wydział";
                worksheet.Cells[1, 5].Value = "Stanowisko";

                // Dodaj dane
                for (int i = 0; i < uzytkownicy.Count; i++)
                {
                    var uzytkownik = uzytkownicy[i];

                    worksheet.Cells[i + 2, 1].Value = uzytkownik.Imie;
                    worksheet.Cells[i + 2, 2].Value = uzytkownik.Nazwisko;
                    worksheet.Cells[i + 2, 3].Value = uzytkownik.NrKontrolny;
                    worksheet.Cells[i + 2, 4].Value = uzytkownik.Wydzialy?.NazwaWydzialu;
                    worksheet.Cells[i + 2, 5].Value = uzytkownik.Stanowiska?.NazwaStanowiska;
                }

                // Zapisz plik
                var stream = new MemoryStream();
                package.SaveAs(stream);

                // Zwróć plik
                var fileName = "Uzytkownicy.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
        }
        // GET: Uzytkownicy
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserViewModel>();
            foreach (Uzytkownik uzytkownik in users)
            {
                var thisViewModel = new UserViewModel();
                thisViewModel.UserId = uzytkownik.Id;
                thisViewModel.UserName = uzytkownik.UserName;
                thisViewModel.FirstName = uzytkownik.Imie;
                thisViewModel.LastName = uzytkownik.Nazwisko;
                thisViewModel.Email= uzytkownik.Email;
                thisViewModel.NrKontrolny = uzytkownik.NrKontrolny;
                thisViewModel.Wydzial = _context.Wydzialy.FirstOrDefault(x => x.WydzialId == uzytkownik.WydzialId).NazwaWydzialu;
                thisViewModel.Stanowisko = _context.Stanowiska.FirstOrDefault(x => x.StanowiskoId == uzytkownik.StanowiskoId).NazwaStanowiska;
                thisViewModel.Roles = await _userManager.GetRolesAsync(uzytkownik);
                thisViewModel.Liczba_narzedzi = _context.Narzedzia.Where(x => x.UzytkownikId == uzytkownik.Id).Count();
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }

        // GET: Uzytkownicy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Uzytkownicy/Create
        public IActionResult Create()
        {
			ViewBag.Stanowisko = new SelectList(_context.Stanowiska.Where(c => c.Active == true).ToList(), "StanowiskoId", "NazwaStanowiska");
			ViewBag.Wydzial = new SelectList(_context.Wydzialy.Where(c => c.Active == true).ToList(), "WydzialId", "NazwaWydzialu");
			return View();
        }

        // POST: Uzytkownicy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Imie,Nazwisko,NrKontrolny,WydzialId,StanowiskoId")] Uzytkownik uzytkownik, string pass)
        {
            if (ModelState.IsValid)
            {
                uzytkownik.UserName = uzytkownik.Email;
                uzytkownik.NormalizedUserName = uzytkownik.UserName;
                uzytkownik.EmailConfirmed = true;
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, pass);
                uzytkownik.PasswordHash = hashed;

                _userManager.CreateAsync(uzytkownik).Wait();
                _userManager.AddToRoleAsync(uzytkownik, "pracownik").Wait();

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uzytkownik);
        }

		// GET: Uzytkownicy/Edit/5
		public async Task<IActionResult> Edit(string? id)
		{
			if (id == null || _context.Uzytkownicy == null)
			{
				return NotFound();
			}

            var uzytkownik = await _context.Uzytkownicy.FindAsync(id);
            IEnumerable<string> user_roles = await _userManager.GetRolesAsync(uzytkownik);
            var role_name = user_roles.FirstOrDefault();
            var role = await _roleManager.FindByNameAsync(role_name);
            ViewBag.Stanowisko = new SelectList(_context.Stanowiska.ToList(), "StanowiskoId", "NazwaStanowiska", uzytkownik.StanowiskoId.ToString());
            ViewBag.Wydzial = new SelectList(_context.Wydzialy.ToList(), "WydzialId", "NazwaWydzialu", uzytkownik.WydzialId.ToString());
			ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Id", "Name", role.Id.ToString());
			//var uzytkownik = await _userManager.Users.FirstAsync(e => e.Id == Id);
			if (uzytkownik == null)
			{
				return NotFound();
			}
			return View(uzytkownik);
		}

		// POST: Uzytkownicy/Edit/5
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ConcurrencyStamp,PasswordHash,Email,Imie,Nazwisko,NrKontrolny,WydzialId,StanowiskoId")] Uzytkownik uzytkownik, string? pass, string role)
        {
			if (id != uzytkownik.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Entry(uzytkownik).State = EntityState.Modified;
					uzytkownik.UserName = uzytkownik.Email;
					uzytkownik.NormalizedUserName = uzytkownik.UserName;
					uzytkownik.EmailConfirmed = true;

                    var selected_role_name = await _roleManager.FindByIdAsync(role);

                    if (pass != null)
                    {
                        var password = new PasswordHasher<Uzytkownik>();
                        var hashed = password.HashPassword(uzytkownik, pass);
                        uzytkownik.PasswordHash = hashed;
                    }

                    if (!await _userManager.IsInRoleAsync(uzytkownik, selected_role_name.ToString()))
                    {
						IEnumerable<string> user_roles = await _userManager.GetRolesAsync(uzytkownik);
						var role_name = user_roles.FirstOrDefault();
						await _userManager.RemoveFromRoleAsync(uzytkownik, role_name);
                        await _userManager.AddToRoleAsync(uzytkownik, selected_role_name.ToString());
                    }

                    _userManager.UpdateAsync(uzytkownik).Wait();

                    await _context.SaveChangesAsync();
                }
				catch (DbUpdateConcurrencyException)
				{
                    if (!UserExists(uzytkownik.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				return RedirectToAction(nameof(Index));
			}
			return View(uzytkownik);
		}
	

        // GET: Uzytkownicy/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Uzytkownicy == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }
            //Tu wstawic sprawdzenie
            return View(uzytkownik);
        }

        // POST: Uzytkownicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Uzytkownicy == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Uzytkownicy'  is null.");
            }
            var uzytkownik = await _context.Uzytkownicy.FindAsync(id);
            if (uzytkownik != null)
            {
                await _userManager.DeleteAsync(uzytkownik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Uzytkownicy.Any(e => e.Id == id);
        }
    }
}