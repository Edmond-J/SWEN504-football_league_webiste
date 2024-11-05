﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballLeagueWebsite.Data;
using FootballLeagueWebsite.Models;

namespace FootballLeagueWebsite.Controllers {

	public class TeamsController : Controller {
		private readonly ApplicationDbContext _context;

		public TeamsController(ApplicationDbContext context) {
			_context = context;
		}

		// GET: Teams
		public async Task<IActionResult> Index() {
			return View(await _context.Team.Include(t => t.Players).ToListAsync());
		}

		// GET: Teams/Details
		public async Task<IActionResult> Details(int? id) {
			if (id == null) {
				return NotFound();
			}

			var team = await _context.Team.Include(t => t.Players)
				.FirstOrDefaultAsync(m => m.TeamId == id);
			if (team == null) {
				return NotFound();
			}

			return View(team);
		}

		public async Task<IActionResult> DetailsByName(string? name) {
			if (name == null) {
				return NotFound();
			}

			var team = await _context.Team.Include(t => t.Players)
				.FirstOrDefaultAsync(m => m.TeamName.Equals(name));
			if (team == null) {
				return NotFound();
			}

			return View(team);
		}

		[Authorize]
		public IActionResult Create() {
			var players = _context.Player.Include(p => p.Team).Select(t => new { Value = t.PlayerName, Text = t.PlayerName }).ToList();
			ViewBag.AllPlayers = players;
			return View();
		}

		// POST: Teams/Create
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Team team) {
			if (ModelState.IsValid) {
				string filePath = "wwwroot/resource/teams/" + team.TeamName + ".png";
				if (team.Logo != null && team.Logo.Length > 0) {
					using (var fileStream = new FileStream(filePath, FileMode.Create)) {
						await team.Logo.CopyToAsync(fileStream);
					}
					team.LogoPath = team.TeamName + ".png";
				} else {
					team.LogoPath = "Logo-Missing.png";
				}
				_context.Add(team);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(team);
		}

		// GET: Teams/Edit
		[Authorize]
		public async Task<IActionResult> Edit(int? id) {
			if (id == null) {
				return NotFound();
			}

			var team = await _context.Team.FindAsync(id);
			if (team == null) {
				return NotFound();
			}
			return View(team);
		}

		// POST: Teams/Edit
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Team team) {
			if (id != team.TeamId) {
				return NotFound();
			}
			if (ModelState.IsValid) {
				if (team.Logo != null && team.Logo.Length > 0) {
					if (team.LogoPath != null && !team.LogoPath.Contains("Logo-Missing")) {
						System.IO.File.Delete("wwwroot/resource/teams/" + team.LogoPath);
					}
					string filePath = "wwwroot/resource/teams/" + team.TeamName + ".png";
					using (var fileStream = new FileStream(filePath, FileMode.Create)) {
						await team.Logo.CopyToAsync(fileStream);
					}
					team.LogoPath = team.TeamName + ".png";
				}
				try {
					_context.Update(team);
					await _context.SaveChangesAsync();
				} catch (DbUpdateConcurrencyException) {
					if (!TeamExists(team.TeamId)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(team);
		}

		// GET: Teams/Delete
		[Authorize]
		public async Task<IActionResult> Delete(int? id) {
			if (id == null) {
				return NotFound();
			}

			var team = await _context.Team.Include(t => t.Players)
				.FirstOrDefaultAsync(m => m.TeamId == id);
			if (team == null) {
				return NotFound();
			}

			return View(team);
		}

		// POST: Teams/Delete
		[Authorize]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id) {
			var team = await _context.Team.FindAsync(id);
			if (team != null) {
				_context.Team.Remove(team);
				if (team.Logo != null) {
					System.IO.File.Delete("wwwroot/resource/teams/" + team.LogoPath);
				}
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool TeamExists(int id) {
			return _context.Team.Any(e => e.TeamId == id);
		}

		//POST: Teams/ShowSearchResult
		public async Task<IActionResult> ShowSearchResult(string SearchPhase) {
			ViewData["SearchPhase"] = SearchPhase;
			return View("Index", await _context.Team.Include(t => t.Players).Where(j => j.TeamName.Contains(SearchPhase)).ToListAsync());
		}
	}
}