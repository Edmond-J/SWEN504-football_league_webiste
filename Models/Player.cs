using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballLeagueWebsite.Models {

    public enum Position {
        Forward, Defender, Midfielder, Goalkeeper
    }

    public class Player {

        [Key]
        public int PlayerId { get; set; }

        [MaxLength(30)]
        public string PlayerName { get; set; }

        [Range(0, 300, ErrorMessage = "Height must be between 0 and 300.")]
        public double Hight { get; set; }

        //public int Age {
        //    get { return DateTime.Now.Year - YearOfBirth; }
        //}
        [NotMapped]
        public int Age => DateTime.Now.Year - YearOfBirth;

        [Range(1924, 2024, ErrorMessage = "Year Of Birth must be between 1924 and 2024.")]
        public int YearOfBirth { get; set; }

        [ForeignKey("TeamId")]
        public int TeamId { get; set; }

        public Team? Team { get; set; }

        [Range(0, 100, ErrorMessage = "Number must be between 0 and 100.")]
        public int Number { get; set; }

        public Position Position { get; set; }
        public string? PhotoPath { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public Player() {
        }
    }
}