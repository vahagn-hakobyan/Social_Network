using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication22_06.Migrations
{
    public partial class chatting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Messenger");

            migrationBuilder.CreateTable(
                name: "Messenger",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1Id = table.Column<int>(nullable: false),
                    User2Id = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messenger", x => x.id);
                    table.ForeignKey(
                        name: "FK_Messenger_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Messenger_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messenger_User1Id",
                table: "Messenger",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messenger_User2Id",
                table: "Messenger",
                column: "User2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messenger");

            //migrationBuilder.CreateTable(
            //    name: "Namakner",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        User1Id = table.Column<int>(type: "int", nullable: false),
            //        User2Id = table.Column<int>(type: "int", nullable: false),
            //        status = table.Column<int>(type: "int", nullable: false),
            //        time = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Namakner", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_Namakner_Users_User1Id",
            //            column: x => x.User1Id,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Namakner_Users_User2Id",
            //            column: x => x.User2Id,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Namakner_User1Id",
            //    table: "Namakner",
            //    column: "User1Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Namakner_User2Id",
            //    table: "Namakner",
            //    column: "User2Id");
        }
    }
}
