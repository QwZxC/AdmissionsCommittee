using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionsCommittee.Migrations
{
    /// <inheritdoc />
    public partial class EnrolleDistrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_PlaceOfResidence_PlaceOfResidenceId",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Certificate_CertificateId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Citizenship_CitizenshipId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Disability_DisabilityId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Education_EducationId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_PlaceOfResidence_PlaceOfResidenceId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Speciality_SpecialityId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Ward_WardId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_District_PlaceOfResidenceId",
                table: "District");

            migrationBuilder.DropColumn(
                name: "PlaceOfResidenceId",
                table: "District");

            migrationBuilder.AlterColumn<DateTime>(
                name: "YearOfAdmission",
                table: "Enrollee",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "WardId",
                table: "Enrollee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityId",
                table: "Enrollee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceOfResidenceId",
                table: "Enrollee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Enrollee",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Enrollee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DisabilityId",
                table: "Enrollee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Enrollee",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "CitizenshipId",
                table: "Enrollee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CertificateId",
                table: "Enrollee",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Enrollee",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_DistrictId",
                table: "Enrollee",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Certificate_CertificateId",
                table: "Enrollee",
                column: "CertificateId",
                principalTable: "Certificate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Citizenship_CitizenshipId",
                table: "Enrollee",
                column: "CitizenshipId",
                principalTable: "Citizenship",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Disability_DisabilityId",
                table: "Enrollee",
                column: "DisabilityId",
                principalTable: "Disability",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_District_DistrictId",
                table: "Enrollee",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Education_EducationId",
                table: "Enrollee",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_PlaceOfResidence_PlaceOfResidenceId",
                table: "Enrollee",
                column: "PlaceOfResidenceId",
                principalTable: "PlaceOfResidence",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Speciality_SpecialityId",
                table: "Enrollee",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Ward_WardId",
                table: "Enrollee",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Certificate_CertificateId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Citizenship_CitizenshipId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Disability_DisabilityId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_District_DistrictId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Education_EducationId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_PlaceOfResidence_PlaceOfResidenceId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Speciality_SpecialityId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Ward_WardId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_DistrictId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Enrollee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "YearOfAdmission",
                table: "Enrollee",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "WardId",
                table: "Enrollee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityId",
                table: "Enrollee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceOfResidenceId",
                table: "Enrollee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Enrollee",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Enrollee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DisabilityId",
                table: "Enrollee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Enrollee",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "CitizenshipId",
                table: "Enrollee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CertificateId",
                table: "Enrollee",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceOfResidenceId",
                table: "District",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_District_PlaceOfResidenceId",
                table: "District",
                column: "PlaceOfResidenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_District_PlaceOfResidence_PlaceOfResidenceId",
                table: "District",
                column: "PlaceOfResidenceId",
                principalTable: "PlaceOfResidence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Certificate_CertificateId",
                table: "Enrollee",
                column: "CertificateId",
                principalTable: "Certificate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Citizenship_CitizenshipId",
                table: "Enrollee",
                column: "CitizenshipId",
                principalTable: "Citizenship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Disability_DisabilityId",
                table: "Enrollee",
                column: "DisabilityId",
                principalTable: "Disability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Education_EducationId",
                table: "Enrollee",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_PlaceOfResidence_PlaceOfResidenceId",
                table: "Enrollee",
                column: "PlaceOfResidenceId",
                principalTable: "PlaceOfResidence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Speciality_SpecialityId",
                table: "Enrollee",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Ward_WardId",
                table: "Enrollee",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
