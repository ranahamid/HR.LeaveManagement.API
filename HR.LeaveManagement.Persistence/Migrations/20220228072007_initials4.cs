using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    public partial class initials4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 7, 20, 7, 123, DateTimeKind.Utc).AddTicks(4981), new DateTime(2022, 2, 28, 7, 20, 7, 123, DateTimeKind.Utc).AddTicks(4982) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 7, 20, 7, 123, DateTimeKind.Utc).AddTicks(4984), new DateTime(2022, 2, 28, 7, 20, 7, 123, DateTimeKind.Utc).AddTicks(4984) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 4, 58, 42, 981, DateTimeKind.Utc).AddTicks(9346), new DateTime(2022, 2, 28, 4, 58, 42, 981, DateTimeKind.Utc).AddTicks(9350) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2022, 2, 28, 4, 58, 42, 981, DateTimeKind.Utc).AddTicks(9353), new DateTime(2022, 2, 28, 4, 58, 42, 981, DateTimeKind.Utc).AddTicks(9354) });
        }
    }
}
