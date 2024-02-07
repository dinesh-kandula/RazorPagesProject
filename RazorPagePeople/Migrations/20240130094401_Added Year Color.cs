using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagePeople.Migrations
{
    /// <inheritdoc />
    public partial class AddedYearColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Categories");
        }
    }
}
