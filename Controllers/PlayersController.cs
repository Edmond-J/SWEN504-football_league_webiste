using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TeamPartnerWebApp.Data;
using TeamPartnerWebApp.Models;

namespace TeamPartnerWebApp.Controllers {

	public class PlayersController : Controller {
		private readonly ApplicationDbContext _context;
		private const string PhotoFolder = "wwwroot/resource/players/";

		public PlayersController(ApplicationDbContext context) {
			_context = context;
		}

		// GET: Players
		public async Task<IActionResult> Index() {
			return View(await _context.Player.Include(p => p.Team).ToListAsync());
		}

		// GET: Players/Details/5
		public async Task<IActionResult> Details(int? id) {
			if (id == null) {
				return NotFound();
			}
			var player = await _context.Player
				.Include(p => p.Team)
				.FirstOrDefaultAsync(m => m.PlayerId == id);
			if (player == null) {
				return NotFound();
			}
			return View(player);
		}

		// GET: Players/Create
		[Authorize]
		public IActionResult Create() {
			//var teams = _context.Team.ToList();
			var teams = _context.Team.Select(t => new { Value = t.TeamId, Text = t.TeamName }).ToList();
			ViewBag.Teams = teams;
			return View();
		}

		// POST: Players/Create
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Player player) {
			//if (player.Image == null)
			//	return "no image";
			//else
			//	return player.Image.FileName;
			//Team team = _context.Team.Include(t => t.Players).FirstOrDefault(t => t.TeamId == player.TeamId);
			//if (team.Players == null) {
			//    team.Players = new List<Player>();
			//}
			//player.Team = team;
			//team.Players.Add(player);
			if (ModelState.IsValid) {
				if (player.Image != null && player.Image.Length > 0) {
					string uniqueFileName = player.PlayerName + "-" + player.YearOfBirth + "-" + player.Team + ".png";
					string filePath = PhotoFolder + uniqueFileName;
					using (var fileStream = new FileStream(filePath, FileMode.Create)) {
						await player.Image.CopyToAsync(fileStream);
					}
					player.PhotoPath = uniqueFileName;
				}
				_context.Add(player);
				await _context.SaveChangesAsync();
				//team = _context.Team.Include(t => t.Players).FirstOrDefault(t => t.TeamId == player.TeamId);
				return RedirectToAction(nameof(Index));
				//return RedirectToAction(nameof(Index), new { teamId = player.TeamId });
			}
			return View(player);
		}

		// GET: Players/Edit
		[Authorize]
		public async Task<IActionResult> Edit(int? id) {
			if (id == null) {
				return NotFound();
			}

			var player = await _context.Player.FindAsync(id);
			if (player == null) {
				return NotFound();
			}
			var teams = _context.Team.Select(t => new { Value = t.TeamId, Text = t.TeamName }).ToList();
			ViewBag.Teams = teams;
			return View(player);
		}

		// POST: Players/Edit
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Player player) {
			if (id != player.PlayerId) {
				return NotFound();
			}
			if (ModelState.IsValid) {
				if (player.Image != null && player.Image.Length > 0) {
					if (player.PhotoPath != null) {
						System.IO.File.Delete(PhotoFolder + player.PhotoPath);
					}
					string uniqueFileName = player.PlayerName + "-" + player.YearOfBirth + "-" + player.Team + ".png";
					string filePath = PhotoFolder + uniqueFileName;
					using (var fileStream = new FileStream(filePath, FileMode.Create)) {
						await player.Image.CopyToAsync(fileStream);
					}
					player.PhotoPath = uniqueFileName;
				}
				try {
					_context.Update(player);
					await _context.SaveChangesAsync();
				} catch (DbUpdateConcurrencyException) {
					if (!PlayerExists(player.PlayerId)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(player);
		}

		// GET: Players/Delete
		[Authorize]
		public async Task<IActionResult> Delete(int? id) {
			if (id == null) {
				return NotFound();
			}

			var player = await _context.Player.Include(p => p.Team)
				.FirstOrDefaultAsync(m => m.PlayerId == id);
			if (player == null) {
				return NotFound();
			}

			return View(player);
		}

		// POST: Players/Delete
		[Authorize]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id) {
			var player = await _context.Player.FindAsync(id);
			if (player != null) {
				_context.Player.Remove(player);
				if (player.PhotoPath != null) {
					System.IO.File.Delete(PhotoFolder + player.PhotoPath);
				}
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PlayerExists(int id) {
			return _context.Player.Any(e => e.PlayerId == id);
		}

		//POST: Players/ShowSearchResult
		public async Task<IActionResult> ShowSearchResult(string SearchPhase) {
			ViewData["SearchPhase"] = SearchPhase;
			return View("Index", await _context.Player.Include(p => p.Team).Where(j => j.PlayerName.Contains(SearchPhase)).ToListAsync());
		}
	}
}