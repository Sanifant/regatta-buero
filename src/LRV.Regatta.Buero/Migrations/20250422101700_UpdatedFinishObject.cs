using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRV.Regatta.Buero.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFinishObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "FinishObjects",
                newName: "SecondPath");

            migrationBuilder.AddColumn<string>(
                name: "FirstPath",
                table: "FinishObjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPath",
                table: "FinishObjects");

            migrationBuilder.RenameColumn(
                name: "SecondPath",
                table: "FinishObjects",
                newName: "Path");
        }
    }
}
