using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "LogEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_EquipmentId",
                table: "LogEntries",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogEntries_Equipment_EquipmentId",
                table: "LogEntries",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogEntries_Equipment_EquipmentId",
                table: "LogEntries");

            migrationBuilder.DropIndex(
                name: "IX_LogEntries_EquipmentId",
                table: "LogEntries");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "LogEntries");
        }
    }
}
