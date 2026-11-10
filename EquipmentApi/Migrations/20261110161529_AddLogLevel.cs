using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentApi.Migrations
{
    /// <inheritdoc />
    public partial class AddLogLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "LogEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "LogEntries");
        }
    }
}
