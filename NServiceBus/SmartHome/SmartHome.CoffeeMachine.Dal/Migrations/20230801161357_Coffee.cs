using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHome.CoffeeMachine.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Coffee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffees_CoffeeMachines_CoffeeMachineId",
                table: "Coffees");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoffeeMachineId",
                table: "Coffees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coffees_CoffeeMachines_CoffeeMachineId",
                table: "Coffees",
                column: "CoffeeMachineId",
                principalTable: "CoffeeMachines",
                principalColumn: "CoffeeMachineId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffees_CoffeeMachines_CoffeeMachineId",
                table: "Coffees");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoffeeMachineId",
                table: "Coffees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffees_CoffeeMachines_CoffeeMachineId",
                table: "Coffees",
                column: "CoffeeMachineId",
                principalTable: "CoffeeMachines",
                principalColumn: "CoffeeMachineId");
        }
    }
}
