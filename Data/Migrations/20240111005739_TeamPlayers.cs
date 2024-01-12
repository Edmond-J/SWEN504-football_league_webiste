using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamPartnerWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class TeamPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamMember",
                table: "Team");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamMember",
                table: "Team",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
