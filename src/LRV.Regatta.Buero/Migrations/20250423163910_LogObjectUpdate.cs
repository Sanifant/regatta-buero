using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRV.Regatta.Buero.Migrations
{
    /// <inheritdoc />
    public partial class LogObjectUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "LogObjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClientVersion",
                table: "LogObjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "LogObjects");

            migrationBuilder.DropColumn(
                name: "ClientVersion",
                table: "LogObjects");
        }
    }
}
