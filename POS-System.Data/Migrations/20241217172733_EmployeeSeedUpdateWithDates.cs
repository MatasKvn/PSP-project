using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeSeedUpdateWithDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateOnly(2024, 1, 2));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateOnly(2022, 5, 6));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartDate",
                value: new DateOnly(2024, 11, 4));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartDate",
                value: new DateOnly(2023, 7, 7));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartDate",
                value: new DateOnly(2009, 8, 14));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 17, 17, 27, 32, 700, DateTimeKind.Utc).AddTicks(6790));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 17, 17, 27, 32, 700, DateTimeKind.Utc).AddTicks(6793));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 17, 27, 32, 700, DateTimeKind.Utc).AddTicks(6739));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 17, 27, 32, 700, DateTimeKind.Utc).AddTicks(6744));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 17, 27, 32, 700, DateTimeKind.Utc).AddTicks(6746));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 17, 27, 32, 700, DateTimeKind.Utc).AddTicks(6747));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartDate",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartDate",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartDate",
                value: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 17, 17, 9, 7, 443, DateTimeKind.Utc).AddTicks(1290));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 17, 17, 9, 7, 443, DateTimeKind.Utc).AddTicks(1294));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 17, 9, 7, 443, DateTimeKind.Utc).AddTicks(1217));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 17, 9, 7, 443, DateTimeKind.Utc).AddTicks(1223));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 17, 9, 7, 443, DateTimeKind.Utc).AddTicks(1225));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 17, 17, 9, 7, 443, DateTimeKind.Utc).AddTicks(1227));
        }
    }
}
