using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class NullableTimeslot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_TimeSlots_TimeSlotId",
                table: "ServiceReservations");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSlotId",
                table: "ServiceReservations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 18, 11, 43, 39, 818, DateTimeKind.Utc).AddTicks(292));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 18, 11, 43, 39, 818, DateTimeKind.Utc).AddTicks(294));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 18, 11, 43, 39, 818, DateTimeKind.Utc).AddTicks(240));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 18, 11, 43, 39, 818, DateTimeKind.Utc).AddTicks(243));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 18, 11, 43, 39, 818, DateTimeKind.Utc).AddTicks(244));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 18, 11, 43, 39, 818, DateTimeKind.Utc).AddTicks(244));

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_TimeSlots_TimeSlotId",
                table: "ServiceReservations",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_TimeSlots_TimeSlotId",
                table: "ServiceReservations");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSlotId",
                table: "ServiceReservations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1528));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1458));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1463));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1464));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 18, 9, 47, 28, 570, DateTimeKind.Utc).AddTicks(1465));

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_TimeSlots_TimeSlotId",
                table: "ServiceReservations",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
