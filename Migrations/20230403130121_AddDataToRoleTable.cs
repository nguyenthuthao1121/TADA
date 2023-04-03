using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TADA.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    1, "Admin"
                }
                );
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    2, "SalesStaff"
                }
                );
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    3, "BusinessStaff"
                }
                );
            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "Hometown", "TelephoneNumber", "Address", "AccountId", "RoleId" },
                values: new object[]
                {
                    1, "Nguyễn Quốc Toàn", "2001-11-30", true, "Thừa Thiên Huế", "0935382788", "74 Bàu Năng 1", 3, 1
                }
                );
            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "Hometown", "TelephoneNumber", "Address", "AccountId", "RoleId" },
                values: new object[]
                {
                    2, "Nguyễn Thị Thu Thảo", "2001-05-29", false, "Đà Nẵng", "0968313313", "74 Âu Cơ ", 8, 2
                }
                );
            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "Hometown", "TelephoneNumber", "Address", "AccountId", "RoleId" },
                values: new object[]
                {
                    3, "Trương Thoại Vỹ", "2000-01-01", true, "Quảng Bình", "0777888999", "74 Nguyễn Tất Thành", 9, 3
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
