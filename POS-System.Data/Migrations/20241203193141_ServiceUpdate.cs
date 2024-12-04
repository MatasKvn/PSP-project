using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace POS_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class ServiceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductOnTax",
                keyColumns: new[] { "ProductVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 1, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(260), 1 });

            migrationBuilder.DeleteData(
                table: "ProductOnTax",
                keyColumns: new[] { "ProductVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 1, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(329), 4 });

            migrationBuilder.DeleteData(
                table: "ProductOnTax",
                keyColumns: new[] { "ProductVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 3, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(338), 3 });

            migrationBuilder.DeleteData(
                table: "ProductOnTax",
                keyColumns: new[] { "ProductVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 4, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(334), 4 });

            migrationBuilder.DeleteData(
                table: "ServiceOnTax",
                keyColumns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 1, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(449), 4 });

            migrationBuilder.DeleteData(
                table: "ServiceOnTax",
                keyColumns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 2, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(465), 3 });

            migrationBuilder.DeleteData(
                table: "ServiceOnTax",
                keyColumns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 4, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(457), 1 });

            migrationBuilder.DeleteData(
                table: "ServiceOnTax",
                keyColumns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 4, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(461), 4 });

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Services");

            migrationBuilder.InsertData(
                table: "ProductOnTax",
                columns: new[] { "ProductVersionId", "StartDate", "TaxVersionId", "EndDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3269), 1, null },
                    { 1, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3338), 4, null },
                    { 3, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3346), 3, null },
                    { 4, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3342), 4, null }
                });

            migrationBuilder.InsertData(
                table: "ServiceOnTax",
                columns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId", "EndDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3435), 4, null },
                    { 2, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3446), 3, null },
                    { 4, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3439), 1, null },
                    { 4, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3443), 4, null }
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "Version",
                value: new DateTime(2024, 11, 1, 15, 30, 30, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3669));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3674));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3678));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3681));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductOnTax",
                keyColumns: new[] { "ProductVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 1, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3269), 1 });

            migrationBuilder.DeleteData(
                table: "ProductOnTax",
                keyColumns: new[] { "ProductVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 1, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3338), 4 });

            migrationBuilder.DeleteData(
                table: "ProductOnTax",
                keyColumns: new[] { "ProductVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 3, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3346), 3 });

            migrationBuilder.DeleteData(
                table: "ProductOnTax",
                keyColumns: new[] { "ProductVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 4, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3342), 4 });

            migrationBuilder.DeleteData(
                table: "ServiceOnTax",
                keyColumns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 1, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3435), 4 });

            migrationBuilder.DeleteData(
                table: "ServiceOnTax",
                keyColumns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 2, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3446), 3 });

            migrationBuilder.DeleteData(
                table: "ServiceOnTax",
                keyColumns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 4, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3439), 1 });

            migrationBuilder.DeleteData(
                table: "ServiceOnTax",
                keyColumns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId" },
                keyValues: new object[] { 4, new DateTime(2024, 12, 3, 21, 31, 40, 831, DateTimeKind.Local).AddTicks(3443), 4 });

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Services",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ProductOnTax",
                columns: new[] { "ProductVersionId", "StartDate", "TaxVersionId", "EndDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(260), 1, null },
                    { 1, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(329), 4, null },
                    { 3, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(338), 3, null },
                    { 4, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(334), 4, null }
                });

            migrationBuilder.InsertData(
                table: "ServiceOnTax",
                columns: new[] { "ServiceVersionId", "StartDate", "TaxVersionId", "EndDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(449), 4, null },
                    { 2, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(465), 3, null },
                    { 4, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(457), 1, null },
                    { 4, new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(461), 4, null }
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "ServiceId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "ServiceId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "ServiceId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ServiceId", "Version" },
                values: new object[] { 2, new DateTime(2024, 11, 1, 15, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(838));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(847));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(851));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 11, 26, 22, 15, 55, 207, DateTimeKind.Local).AddTicks(855));
        }
    }
}
