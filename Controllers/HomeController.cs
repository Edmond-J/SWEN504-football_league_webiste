using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamPartnerWebApp.Models;

namespace TeamPartnerWebApp.Controllers {

	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger) {
			_logger = logger;
		}

		public IActionResult Index() {
			return View();
		}

		public IActionResult Privacy() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		//GET: Players/Search
		public async Task<IActionResult> Search() {
			return View();
		}

		public IActionResult ChoseSearchEngine(string SearchBy, string SearchPhase) {
			if (SearchBy.Equals("player")) {
				return RedirectToAction("ShowSearchResult", "Players", new { SearchPhase });
			} else if (SearchBy.Equals("team")) {
				return RedirectToAction("ShowSearchResult", "Teams", new { SearchPhase });
			} else {
				return View();
			}
		}
	}
}