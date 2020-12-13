using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagment.Data.Migrations
{
    public partial class NoFullname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Member");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Member",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
