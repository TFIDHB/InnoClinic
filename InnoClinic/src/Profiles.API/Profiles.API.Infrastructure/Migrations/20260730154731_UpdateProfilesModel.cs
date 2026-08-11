using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfilesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistProfiles_AccountId",
                table: "ReceptionistProfiles",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_AccountId",
                table: "PatientProfiles",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_AccountId",
                table: "DoctorProfiles",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReceptionistProfiles_AccountId",
                table: "ReceptionistProfiles");

            migrationBuilder.DropIndex(
                name: "IX_PatientProfiles_AccountId",
                table: "PatientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_DoctorProfiles_AccountId",
                table: "DoctorProfiles");
        }
    }
}
