using Microsoft.EntityFrameworkCore.Migrations;

namespace Majunga.Server.Data.Migrations
{
    public partial class ShareLinkColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShareLink",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShareLink",
                table: "Files");
        }
    }
}
