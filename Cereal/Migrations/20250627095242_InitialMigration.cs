using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cereal.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cereals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Calories = table.Column<int>(type: "INTEGER", nullable: false),
                    Protein = table.Column<int>(type: "INTEGER", nullable: false),
                    Fat = table.Column<int>(type: "INTEGER", nullable: false),
                    Sodium = table.Column<int>(type: "INTEGER", nullable: false),
                    Fiber = table.Column<float>(type: "REAL", nullable: false),
                    Carbo = table.Column<float>(type: "REAL", nullable: false),
                    Sugars = table.Column<int>(type: "INTEGER", nullable: false),
                    Potass = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitamins = table.Column<int>(type: "INTEGER", nullable: false),
                    Shelf = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<float>(type: "REAL", nullable: false),
                    Cups = table.Column<float>(type: "REAL", nullable: false),
                    Rating = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cereals", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cereals");
        }
    }
}
