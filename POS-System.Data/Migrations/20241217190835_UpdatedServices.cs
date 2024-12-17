using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Services",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 17, 19, 8, 34, 765, DateTimeKind.Utc).AddTicks(8736));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 17, 19, 8, 34, 765, DateTimeKind.Utc).AddTicks(8739));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "EmployeeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "EmployeeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "EmployeeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "EmployeeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 19, 8, 34, 765, DateTimeKind.Utc).AddTicks(8704));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 19, 8, 34, 765, DateTimeKind.Utc).AddTicks(8707));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 19, 8, 34, 765, DateTimeKind.Utc).AddTicks(8708));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 19, 8, 34, 765, DateTimeKind.Utc).AddTicks(8709));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Services");

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 16, 13, 31, 49, 459, DateTimeKind.Utc).AddTicks(6292));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 16, 13, 31, 49, 459, DateTimeKind.Utc).AddTicks(6298));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 16, 13, 31, 49, 459, DateTimeKind.Utc).AddTicks(6170));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 16, 13, 31, 49, 459, DateTimeKind.Utc).AddTicks(6180));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 16, 13, 31, 49, 459, DateTimeKind.Utc).AddTicks(6183));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 16, 13, 31, 49, 459, DateTimeKind.Utc).AddTicks(6186));
        }
    }
}
