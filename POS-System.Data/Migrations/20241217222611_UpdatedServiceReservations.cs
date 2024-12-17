using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedServiceReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCancelled",
                table: "ServiceReservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 17, 22, 26, 10, 825, DateTimeKind.Utc).AddTicks(4224));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 17, 22, 26, 10, 825, DateTimeKind.Utc).AddTicks(4226));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 22, 26, 10, 825, DateTimeKind.Utc).AddTicks(4188));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 22, 26, 10, 825, DateTimeKind.Utc).AddTicks(4190));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 22, 26, 10, 825, DateTimeKind.Utc).AddTicks(4191));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 22, 26, 10, 825, DateTimeKind.Utc).AddTicks(4192));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCancelled",
                table: "ServiceReservations");

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
    }
}
