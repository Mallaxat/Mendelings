using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mendelings.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneticTraits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DominantAllele = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    RecessiveAllele = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DominantPhenotype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecessivePhenotype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DominantAssetPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecessiveAssetPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneticTraits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherId = table.Column<int>(type: "int", nullable: true),
                    FatherId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Generation = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Hunger = table.Column<int>(type: "int", nullable: false),
                    Mood = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TailGene = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    EarsGene = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    EyesGene = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    BodyGene = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    HeadGene = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    HornsGene = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Pets_FatherId",
                        column: x => x.FatherId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pets_Pets_MotherId",
                        column: x => x.MotherId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneticTraits_Code",
                table: "GeneticTraits",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_FatherId",
                table: "Pets",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_MotherId",
                table: "Pets",
                column: "MotherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneticTraits");

            migrationBuilder.DropTable(
                name: "Pets");
        }
    }
}
