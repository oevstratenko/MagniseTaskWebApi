using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagniseTaskDAC.Migrations
{
    public partial class MagniseTaskInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crypto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Price_usd = table.Column<decimal>(type: "numeric(18,5)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crypto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CryptoId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Price_usd = table.Column<decimal>(type: "numeric(18,5)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryptoHistory_Crypto_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "Crypto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoHistory_CryptoId",
                table: "CryptoHistory",
                column: "CryptoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoHistory");

            migrationBuilder.DropTable(
                name: "Crypto");
        }
    }
}
