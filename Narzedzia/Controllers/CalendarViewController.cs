using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Narzedzia.Data;
using Narzedzia.Helpers;
using Narzedzia.Models;
using Narzedzia.Models.ViewModels;
using Narzedzia.Controllers;
using Microsoft.AspNetCore.Authorization;


namespace Narzedzia.Controllers
{
    [Authorize(Roles = "admin, nadzor")]
    public class CalendarViewController : Controller
    {
        private readonly ILogger<CalendarViewController> _logger;
        private readonly IDAL _idal;
        private readonly IDAL _dal;


        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _dbContext;


        public CalendarViewController(ILogger<CalendarViewController> logger, IDAL idal, IDAL dal, ApplicationDbContext context, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _idal = idal;
            _dal = dal;
            _context = context;
            _dbContext = dbContext;
        }

        public IActionResult CalendarVW()
        {

            var viewModel = new EventsViewModel();

             // Pobierz listę narzędzi z IDAL
    var narzedziaList = _idal.GetNarzedzias();

            // Konwertuj listę narzędzi na listę SelectListItem
            viewModel.Narzedzie = narzedziaList.Select(n => new SelectListItem { Value = n.NarzedzieId.ToString(), Text = n.Nazwa }).ToList();

            // Pobierz listę narzędzi z IDAL
            var wydzialylist = _idal.GetWydzials();

            // Konwertuj listę narzędzi na listę SelectListItem
            viewModel.Wydzial = wydzialylist.Select(n => new SelectListItem { Value = n.WydzialId.ToString(), Text = n.NazwaWydzialu }).ToList();

            // Pobierz listę narzędzi z IDAL
            var stanowiskalist = _idal.GetStanowiskos();

            // Konwertuj listę narzędzi na listę SelectListItem
            viewModel.Stanowisko = stanowiskalist.Select(n => new SelectListItem { Value = n.StanowiskoId.ToString(), Text = n.NazwaStanowiska }).ToList();



            ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(_idal.GetStanowiskos(), _idal.GetWydzials(), _idal.GetNarzedzias());
            ViewData["Events"] = JSONListHelper.GetEventListJSONString(_idal.GetEvents());
/*            return View();
*/            return View(viewModel);

        }





    }
}
