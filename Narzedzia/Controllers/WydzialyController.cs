using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Narzedzia.Data;
using Narzedzia.Models;
using OfficeOpenXml;

namespace Narzedzia.Controllers
{
	[Authorize(Roles = "admin, nadzor")]
	public class WydzialyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDAL _idal;
        public WydzialyController(ApplicationDbContext context, IDAL idal)
        {
            _context = context;
            _idal = idal;

        }
        // Akcja eksportu danych do pliku Excel
        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var wydzialy = _context.Wydzialy
                            .Include(w => w.Uzytkownicy)
                            .Include(w => w.Awarie)
                            .Include(w => w.Events)
                            .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Wydzialy");

                // Dodaj nagłówki
                worksheet.Cells[1, 1].Value = "Nazwa wydziału";
                worksheet.Cells[1, 2].Value = "Aktywny";

                // Dodaj dane
                for (int i = 0; i < wydzialy.Count; i++)
                {
                    var wydzial = wydzialy[i];

                    worksheet.Cells[i + 2, 1].Value = wydzial.NazwaWydzialu;
                    worksheet.Cells[i + 2, 2].Value = wydzial.Active ? "Tak" : "Nie";
                }

                // Zapisz plik
                var stream = new MemoryStream();
                package.SaveAs(stream);

                // Zwróć plik
                var fileName = "Wydzialy.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
        }
        // GET: Wydzialy
        public async Task<IActionResult> Index()
        {
              return View(await _context.Wydzialy.Include(u => u.Uzytkownicy).ToListAsync());
        }

        // GET: Wydzialy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wydzialy == null)
            {
                return NotFound();
            }

            var wydzial = await _context.Wydzialy
                .FirstOrDefaultAsync(m => m.WydzialId == id);
            if (wydzial == null)
            {
                return NotFound();
            }

            return View(wydzial);
        }

        // GET: Wydzialy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wydzialy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WydzialId,NazwaWydzialu,Active")] Wydzial wydzial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wydzial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wydzial);
        }

        // GET: Wydzialy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wydzialy == null)
            {
                return NotFound();
            }

            var wydzial = await _context.Wydzialy.FindAsync(id);
            if (wydzial == null)
            {
                return NotFound();
            }
            return View(wydzial);
        }

        // POST: Wydzialy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WydzialId,NazwaWydzialu,Active")] Wydzial wydzial)
        {
            if (id != wydzial.WydzialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wydzial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WydzialExists(wydzial.WydzialId))
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
            return View(wydzial);
        }

        // GET: Wydzialy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Wydzialy == null)
            {
                return NotFound();
            }

            var wydzial = await _context.Wydzialy
                .FirstOrDefaultAsync(m => m.WydzialId == id);
            if (wydzial == null)
            {
                return NotFound();
            }
            if (WydzialNiepusty((int)id))
            {
                ViewBag.DeleteMessage = "Nie można usunąć wybranego elementu, gdyż posiada przypisanych użytkowników.";
            }

            return View(wydzial);
        }

        // POST: Wydzialy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wydzialy == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Wydzialy'  is null.");
            }
            var wydzial = await _context.Wydzialy.FindAsync(id);
            if (wydzial != null)
            {
                _context.Wydzialy.Remove(wydzial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WydzialExists(int id)
        {
          return _context.Wydzialy.Any(e => e.WydzialId == id);
        }

        private bool WydzialNiepusty(int id)
        {
            return (_context.Uzytkownicy?.Any(t => t.WydzialId == id)).GetValueOrDefault();
        }
    }
}
