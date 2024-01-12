using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamPartnerWebApp.Models {

    public class Team {

        [Key]
        public int TeamId { get; set; }

        public string TeamName { get; set; }
        public string City { get; set; }
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