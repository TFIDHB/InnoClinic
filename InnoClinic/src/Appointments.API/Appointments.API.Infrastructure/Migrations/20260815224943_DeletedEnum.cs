using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeletedEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Appointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: string.Empty);
        }
    }
}
