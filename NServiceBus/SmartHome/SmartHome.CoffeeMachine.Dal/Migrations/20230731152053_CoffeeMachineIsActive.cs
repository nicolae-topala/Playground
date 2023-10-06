using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHome.CoffeeMachine.Dal.Migrations
{
    /// <inheritdoc />
    public partial class CoffeeMachineIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CoffeeMachines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CoffeeMachines");
        }
    }
}
