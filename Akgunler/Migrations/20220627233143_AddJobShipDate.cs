using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akgunler.Migrations
{
    public partial class AddJobShipDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalShipDate",
                schema: "Job",
                table: "Job",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureShipDate",
                schema: "Job",
                table: "Job",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalShipDate",
                schema: "Job",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "DepartureShipDate",
                schema: "Job",
                table: "Job");
        }
    }
}
