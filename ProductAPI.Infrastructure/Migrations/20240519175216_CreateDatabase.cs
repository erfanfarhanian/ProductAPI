using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProduceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManufacturePhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ManufactureEmail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProduceDate_ManufactureEmail",
                table: "Products",
                columns: new[] { "ProduceDate", "ManufactureEmail" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
