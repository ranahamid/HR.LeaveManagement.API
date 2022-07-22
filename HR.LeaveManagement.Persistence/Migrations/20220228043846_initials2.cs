using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    public partial class initials2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 4, 38, 45, 967, DateTimeKind.Utc).AddTicks(346), new DateTime(2022, 2, 28, 4, 38, 45, 967, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 4, 38, 45, 967, DateTimeKind.Utc).AddTicks(352), new DateTime(2022, 2, 28, 4, 38, 45, 967, DateTimeKind.Utc).AddTicks(352) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 4, 38, 30, 27, DateTimeKind.Utc).AddTicks(5203), new DateTime(2022, 2, 28, 4, 38, 30, 27, DateTimeKind.Utc).AddTicks(5205) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 4, 38, 30, 27, DateTimeKind.Utc).AddTicks(5207), new DateTime(2022, 2, 28, 4, 38, 30, 27, DateTimeKind.Utc).AddTicks(5207) });
        }
    }
}
