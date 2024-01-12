using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPartnerWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTeamAsObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Player",
                newName: "YearOfBirth");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamId",
                table: "Player",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_TeamId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "YearOfBirth",
                table: "Player",
                newName: "Age");

            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
