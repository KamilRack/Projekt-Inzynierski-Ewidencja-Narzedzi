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
	public class StanowiskaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDAL _dal;

        public StanowiskaController(ApplicationDbContext context, IDAL dal)
        {
            _context = context;
            _dal = dal;

        }
        // Akcja eksportu danych do pliku Excel
        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var stanowiska = _context.Stanowiska.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Stanowiska");

                // Dodaj nagłówki
                worksheet.Cells[1, 1].Value = "Identyfikator stanowiska";
                worksheet.Cells[1, 2].Value = "Nazwa stanowiska";
                worksheet.Cells[1, 3].Value = "Czy stanowisko aktywne";

                // Dodaj dane
                for (int i = 0; i < stanowiska.Count; i++)
                {
                    var stanowisko = stanowiska[i];

                    worksheet.Cells[i + 2, 1].Value = stanowisko.StanowiskoId;
                    worksheet.Cells[i + 2, 2].Value = stanowisko.NazwaStanowiska;
                    worksheet.Cells[i + 2, 3].Value = stanowisko.Active ? "Tak" : "Nie";
                }

                // Zapisz plik
                var stream = new MemoryStream();
                package.SaveAs(stream);

                // Zwróć plik
                var fileName = "Stanowiska.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
        }
        // GET: Stanowiska
        public async Task<IActionResult> Index()
        {
              return View(await _context.Stanowiska.Include(u => u.Uzytkownicy).ToListAsync());
        }

        // GET: Stanowiska/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stanowiska == null)
            {
                return NotFound();
            }

            var stanowisko = await _context.Stanowiska
                .FirstOrDefaultAsync(m => m.StanowiskoId == id);
            if (stanowisko == null)
            {
                return NotFound();
            }

            return View(stanowisko);
        }

        // GET: Stanowiska/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stanowiska/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StanowiskoId,NazwaStanowiska,Active")] Stanowisko stanowisko)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stanowisko);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stanowisko);
        }

        // GET: Stanowiska/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stanowiska == null)
            {
                return NotFound();
            }

            var stanowisko = await _context.Stanowiska.FindAsync(id);
            if (stanowisko == null)
            {
                return NotFound();
            }
            return View(stanowisko);
        }

        // POST: Stanowiska/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StanowiskoId,NazwaStanowiska,Active")] Stanowisko stanowisko)
        {
            if (id != stanowisko.StanowiskoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stanowisko);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StanowiskoExists(stanowisko.StanowiskoId))
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
            return View(stanowisko);
        }

        // GET: Stanowiska/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stanowiska == null)
            {
                return NotFound();
            }

            var stanowisko = await _context.Stanowiska
                .FirstOrDefaultAsync(m => m.StanowiskoId == id);
            if (stanowisko == null)
            {
                return NotFound();
            }
            if (StanowiskoNiepuste((int)id))
            {
                ViewBag.DeleteMessage = "Nie można usunąć wybranego elementu, gdyż posiada przypisanych użytkowników.";
            }

            return View(stanowisko);
        }

        // POST: Stanowiska/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stanowiska == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Stanowiska'  is null.");
            }
            var stanowisko = await _context.Stanowiska.FindAsync(id);
            if (stanowisko != null)
            {
                _context.Stanowiska.Remove(stanowisko);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StanowiskoExists(int id)
        {
          return _context.Stanowiska.Any(e => e.StanowiskoId == id);
        }

        private bool StanowiskoNiepuste(int id)
        {
            return (_context.Uzytkownicy?.Any(t => t.StanowiskoId == id)).GetValueOrDefault();
        }
    }
}
