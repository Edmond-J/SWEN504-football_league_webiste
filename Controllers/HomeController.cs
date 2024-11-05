using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using FootballLeagueWebsite.Data;
using FootballLeagueWebsite.Models;

namespace FootballLeagueWebsite.Controllers {

	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context) {
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> Index() {
			return View(await _context.Team.Include(t => t.Players).ToListAsync());
		}

		public IActionResult Privacy() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		//GET: Players/Search
		public IActionResult Search() {
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