using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHome.CoffeeMachine.Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeMachines",
                columns: table => new
                {
                    CoffeeMachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoffeeMachineSerialNr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeMachines", x => x.CoffeeMachineId);
                });

            migrationBuilder.CreateTable(
                name: "Coffees",
                columns: table => new
                {
                    CoffeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoffeeMachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coffees", x => x.CoffeeId);
                    table.ForeignKey(
                        name: "FK_Coffees_CoffeeMachines_CoffeeMachineId",
                        column: x => x.CoffeeMachineId,
                        principalTable: "CoffeeMachines",
                        principalColumn: "CoffeeMachineId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_CoffeeMachineId",
                table: "Coffees",
                column: "CoffeeMachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coffees");

            migrationBuilder.DropTable(
                name: "CoffeeMachines");
        }
    }
}
