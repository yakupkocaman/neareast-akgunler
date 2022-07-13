using Microsoft.EntityFrameworkCore.Migrations;

namespace Akgunler.Migrations
{
    public partial class UpdateJobStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobStaff",
                schema: "Job");

            migrationBuilder.DropColumn(
                name: "ExchangeRateEur",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ExchangeRateUsd",
                schema: "Job",
                table: "Job");

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                schema: "Job",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TractorId",
                schema: "Job",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrailerId",
                schema: "Job",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_StaffId",
                schema: "Job",
                table: "Job",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_TractorId",
                schema: "Job",
                table: "Job",
                column: "TractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_TrailerId",
                schema: "Job",
                table: "Job",
                column: "TrailerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Staff_StaffId",
                schema: "Job",
                table: "Job",
                column: "StaffId",
                principalSchema: "Staff",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Vehicle_TractorId",
                schema: "Job",
                table: "Job",
                column: "TractorId",
                principalSchema: "Vehicle",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Vehicle_TrailerId",
                schema: "Job",
                table: "Job",
                column: "TrailerId",
                principalSchema: "Vehicle",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Staff_StaffId",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_Vehicle_TractorId",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_Vehicle_TrailerId",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_StaffId",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_TractorId",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_TrailerId",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "StaffId",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "TractorId",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "TrailerId",
                schema: "Job",
                table: "Job");

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRateEur",
                schema: "Job",
                table: "Job",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRateUsd",
                schema: "Job",
                table: "Job",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "JobStaff",
                schema: "Job",
                columns: table => new
                {
                    JobStaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    TractorId = table.Column<int>(type: "int", nullable: false),
                    TrailerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStaff", x => x.JobStaffId);
                    table.ForeignKey(
                        name: "FK_JobStaff_Job_JobId",
                        column: x => x.JobId,
                        principalSchema: "Job",
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Staff",
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobStaff_Vehicle_TractorId",
                        column: x => x.TractorId,
                        principalSchema: "Vehicle",
                        principalTable: "Vehicle",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobStaff_Vehicle_TrailerId",
                        column: x => x.TrailerId,
                        principalSchema: "Vehicle",
                        principalTable: "Vehicle",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobStaff_JobId",
                schema: "Job",
                table: "JobStaff",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStaff_StaffId",
                schema: "Job",
                table: "JobStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStaff_TractorId",
                schema: "Job",
                table: "JobStaff",
                column: "TractorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStaff_TrailerId",
                schema: "Job",
                table: "JobStaff",
                column: "TrailerId");
        }
    }
}
