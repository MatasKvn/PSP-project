using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PhoneNumber", "RoleId" },
                values: new object[] { "3463466346", 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PhoneNumber", "RoleId" },
                values: new object[] { "77567455", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PhoneNumber", "RoleId" },
                values: new object[] { "4352335255", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PhoneNumber", "RoleId" },
                values: new object[] { "24142141241", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PhoneNumber", "RoleId" },
                values: new object[] { "546646564", 4 });

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 16, 21, 1, 44, 55, DateTimeKind.Utc).AddTicks(316));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 16, 21, 1, 44, 55, DateTimeKind.Utc).AddTicks(319));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 16, 21, 1, 44, 55, DateTimeKind.Utc).AddTicks(262));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 16, 21, 1, 44, 55, DateTimeKind.Utc).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 16, 21, 1, 44, 55, DateTimeKind.Utc).AddTicks(268));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 16, 21, 1, 44, 55, DateTimeKind.Utc).AddTicks(270));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhoneNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PhoneNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PhoneNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PhoneNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "PhoneNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Version",
                value: new DateTime(2024, 12, 10, 21, 4, 14, 623, DateTimeKind.Utc).AddTicks(8825));

            migrationBuilder.UpdateData(
                table: "ItemDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Version",
                value: new DateTime(2024, 12, 10, 21, 4, 14, 623, DateTimeKind.Utc).AddTicks(8827));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 12, 10, 21, 4, 14, 623, DateTimeKind.Utc).AddTicks(8795));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 12, 10, 21, 4, 14, 623, DateTimeKind.Utc).AddTicks(8799));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2024, 12, 10, 21, 4, 14, 623, DateTimeKind.Utc).AddTicks(8799));

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2024, 12, 10, 21, 4, 14, 623, DateTimeKind.Utc).AddTicks(8800));
        }
    }
}
