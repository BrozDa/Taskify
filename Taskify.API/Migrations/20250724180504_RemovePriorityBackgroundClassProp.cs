using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskify.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovePriorityBackgroundClassProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundClass",
                table: "Priorities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundClass",
                table: "Priorities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
