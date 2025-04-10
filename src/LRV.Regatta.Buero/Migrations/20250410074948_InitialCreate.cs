using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LRV.Regatta.Buero.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinishObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Race = table.Column<string>(type: "text", nullable: false),
                    StartNo = table.Column<string>(type: "text", nullable: false),
                    Team = table.Column<string>(type: "text", nullable: false),
                    Position1 = table.Column<string>(type: "text", nullable: true),
                    Position2 = table.Column<string>(type: "text", nullable: true),
                    Position3 = table.Column<string>(type: "text", nullable: true),
                    Position4 = table.Column<string>(type: "text", nullable: true),
                    Position5 = table.Column<string>(type: "text", nullable: true),
                    Position6 = table.Column<string>(type: "text", nullable: true),
                    Position7 = table.Column<string>(type: "text", nullable: true),
                    Position8 = table.Column<string>(type: "text", nullable: true),
                    PositionCox = table.Column<string>(type: "text", nullable: true),
                    ChairMan = table.Column<string>(type: "text", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationObjects", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishObjects");

            migrationBuilder.DropTable(
                name: "RegistrationObjects");
        }
    }
}
