using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagePeople.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserNamePasswordRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Persons");
        }
    }
}
