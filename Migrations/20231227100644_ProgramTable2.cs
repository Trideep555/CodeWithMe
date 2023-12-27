using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeWithMe.Migrations
{
    /// <inheritdoc />
    public partial class ProgramTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Program",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Program",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Program");
        }
    }
}
