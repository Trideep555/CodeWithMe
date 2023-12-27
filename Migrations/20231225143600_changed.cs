using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeWithMe.Migrations
{
    /// <inheritdoc />
    public partial class changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "attachment",
                table: "Languages",
                newName: "Attachment");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Attachment",
                table: "Languages",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(MAX)");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Languages",
                type: "varbinary(MAX)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "Attachment",
                table: "Languages",
                newName: "attachment");

            migrationBuilder.AlterColumn<byte[]>(
                name: "attachment",
                table: "Languages",
                type: "varbinary(MAX)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
