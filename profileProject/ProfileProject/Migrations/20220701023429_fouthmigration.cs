using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileProject.Migrations
{
    public partial class fouthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");
        }
    }
}
