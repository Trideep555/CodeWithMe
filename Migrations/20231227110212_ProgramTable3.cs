using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeWithMe.Migrations
{
    /// <inheritdoc />
    public partial class ProgramTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LangaugeId",
                table: "Program");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LangaugeId",
                table: "Program",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
