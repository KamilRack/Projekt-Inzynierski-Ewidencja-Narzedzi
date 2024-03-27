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
	public class ProducenciController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProducenciController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Akcja eksportu danych do pliku Excel
        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var producenci = _context.Producenci.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Producenci");

                // Dodaj nagłówki
                worksheet.Cells[1, 1].Value = "Identyfikator producenta";
                worksheet.Cells[1, 2].Value = "Nazwa producenta";
                worksheet.Cells[1, 3].Value = "Czy producent aktywny";

                // Dodaj dane
                for (int i = 0; i < producenci.Count; i++)
                {
                    var producent = producenci[i];

                    worksheet.Cells[i + 2, 1].Value = producent.ProducentId;
                    worksheet.Cells[i + 2, 2].Value = producent.NazwaProducenta;
                    worksheet.Cells[i + 2, 3].Value = producent.Active ? "Tak" : "Nie";
                }

                // Zapisz plik
                var stream = new MemoryStream();
                package.SaveAs(stream);

                // Zwróć plik
                var fileName = "Producenci.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
        }

        // GET: Producenci
        public async Task<IActionResult> Index()
        {
              return View(await _context.Producenci.Include(n => n.Narzedzia).ToListAsync());
        }

        // GET: Producenci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producenci == null)
            {
                return NotFound();
            }

            var producent = await _context.Producenci
                .FirstOrDefaultAsync(m => m.ProducentId == id);
            if (producent == null)
            {
                return NotFound();
            }

            return View(producent);
        }

        // GET: Producenci/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producenci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProducentId,NazwaProducenta,Active")] Producent producent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producent);
        }

        // GET: Producenci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producenci == null)
            {
                return NotFound();
            }

            var producent = await _context.Producenci.FindAsync(id);
            if (producent == null)
            {
                return NotFound();
            }
            return View(producent);
        }

        // POST: Producenci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducentId,NazwaProducenta,Active")] Producent producent)
        {
            if (id != producent.ProducentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducentExists(producent.ProducentId))
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
            return View(producent);
        }

        // GET: Producenci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producenci == null)
            {
                return NotFound();
            }

            var producent = await _context.Producenci
                .FirstOrDefaultAsync(m => m.ProducentId == id);
            if (producent == null)
            {
                return NotFound();
            }
            if (ProducentNiepusty((int)id))
            {
                ViewBag.DeleteMessage = "Nie można usunąć wybranego elementu, gdyż posiada przypisane narzędzia.";
            }

            return View(producent);
        }

        // POST: Producenci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producenci == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Producenci'  is null.");
            }
            var producent = await _context.Producenci.FindAsync(id);
            if (producent != null)
            {
                _context.Producenci.Remove(producent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducentExists(int id)
        {
          return _context.Producenci.Any(e => e.ProducentId == id);
        }

        private bool ProducentNiepusty(int id)
        {
            return (_context.Narzedzia?.Any(t => t.ProducentId == id)).GetValueOrDefault();
        }
    }
}
