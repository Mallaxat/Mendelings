using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mendelings.Migrations
{
    /// <inheritdoc />
    public partial class AddPetLifeState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnergyMinutes",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealthMinutes",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HungerMinutes",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSleeping",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MoodMinutes",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SleepStarted",
                table: "Pets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnergyMinutes",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "HealthMinutes",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "HungerMinutes",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IsSleeping",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "MoodMinutes",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "SleepStarted",
                table: "Pets");
        }
    }
}
