using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication22_06.Migrations
{
    public partial class chattingwithcontent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "Messenger",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content",
                table: "Messenger");
        }
    }
}
