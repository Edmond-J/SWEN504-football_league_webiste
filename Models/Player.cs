using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamPartnerWebApp.Models {

	public enum Position {
		Forward, Defender, Midfielder, Goalkeeper
	}

	public class Player {

		[Key]
		public int PlayerId { get; set; }

		public string PlayerName { get; set; }
		public double Hight { get; set; }

		[NotMapped]
		public int Age {
			get { return DateTime.Now.Year - YearOfBirth; }
		}

		public int YearOfBirth { get; set; }

		[ForeignKey("TeamId")]
		public int TeamId { get; set; }

		public Team? Team { get; set; }

		public int Number { get; set; }
		public Position Position { get; set; }
		public string? PhotoPath { get; set; }

		[NotMapped]
		public IFormFile? Image { get; set; }

		public Player() {
		}
	}
}