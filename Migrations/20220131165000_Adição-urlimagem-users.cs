using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_App_Flix.Migrations
{
    public partial class Adiçãourlimagemusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImagem",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImagem",
                table: "Users");
        }
    }
}
