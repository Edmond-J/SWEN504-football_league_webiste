using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballLeagueWebsite.Data.Migrations {

	/// <inheritdoc />
	public partial class InitialCreate : Migration {

		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Player",
				columns: table => new {
					PlayerId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Hight = table.Column<double>(type: "float", nullable: false),
					Age = table.Column<int>(type: "int", nullable: false),
					Team = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Number = table.Column<int>(type: "int", nullable: false),
					Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
					PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Player", x => x.PlayerId);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Player");
		}
	}
}