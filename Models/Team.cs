using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballLeagueWebsite.Models {

	public class Team {

		[Key]
		public int TeamId { get; set; }

		[MinLength(3)]
		[MaxLength(20)]
		public string TeamName { get; set; }

		[MaxLength(20)]
		public string City { get; set; }

		[MaxLength(30)]
		public string Coach { get; set; }

		public List<Player>? Players { get; set; }
		public string? LogoPath { get; set; }

		[NotMapped]
		public IFormFile? Logo { get; set; }

		public Team() {
		}

		public int TeamSize() {
			if (Players == null) return 0;
			return Players.Count;
		}
	}
}