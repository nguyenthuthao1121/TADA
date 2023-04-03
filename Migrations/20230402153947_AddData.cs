using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TADA.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    1, "Danh mục 1"
                }
                );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    2, "Danh mục 2"
                }
                );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    3, "Danh mục 3"
                }
                );

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name", "Price", "Sale", "Image", "CategoryId" },
                values: new object[]
                {
                    1, "Sách 1", 50000, 5, "E:\\PBL3\\TADA\\wwwroot\\img\\book1.jpg", 3
                }
                );

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name", "Price", "Sale", "Image", "CategoryId" },
                values: new object[]
                {
                    2, "Sách 2", 86000, 8, "E:\\PBL3\\TADA\\wwwroot\\img\\book2.jpg", 1
                }
                );

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name", "Price", "Sale", "Image", "CategoryId" },
                values: new object[]
                {
                    3, "Sách 3", 75000, 10, "E:\\PBL3\\TADA\\wwwroot\\img\\book1.jpg", 2
                }
                );

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name", "Price", "Sale", "Image", "CategoryId" },
                values: new object[]
                {
                    4, "Sách 4", 125000, 15, "E:\\PBL3\\TADA\\wwwroot\\img\\book1.jpg", 2
                }
                );

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name", "Price", "Sale", "Image", "CategoryId" },
                values: new object[]
                {
                    5, "Sách 5", 89000, 2, "E:\\PBL3\\TADA\\wwwroot\\img\\book2.jpg", 1
                }
                );



            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    1, true, "customer1@gmail.com", "customer1abc", "2023-03-31", true
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    2, true, "customer2@gmail.com", "123456", "2023-03-30", false
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    3, false, "admin1@gmail.com", "abcxyz", "2023-03-31", true
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    4, true, "toancute@gmail.com", "customer123", "2023-03-31", true
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    5, true, "ntthao@gmail.com", "thuthao", "2023-03-29", true
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    6, true, "thoaivytruong@gmail.com", "thaovy", "2023-03-31", true
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    7, true, "tmynguyen@gmail.com", "tramy", "2023-03-30", true
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    8, false, "admin2@gmail.com", "admin2@123", "2023-03-28", true
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    9, false, "admin3@gmail.com", "111222333", "2023-03-31", true
                }
                );
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Type", "Email", "Password", "CreateDate", "Status" },
                values: new object[]
                {
                    10, true, "customerxyz@gmail.com", "123456", "2023-03-31", true
                }
                );

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "Hometown", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    1, "Nguyễn Quốc Toàn", "2001-11-30", true, "Thừa Thiên Huế", "0935382788", "74 Bàu Năng 1", 3
                }
                );
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "Hometown", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    2, "Nguyễn Thị Thu Thảo", "2001-05-29", false, "Đà Nẵng", "0968313313", "74 Âu Cơ ", 8
                }
                );
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "Hometown", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    3, "Trương Thoại Vỹ", "2000-01-01", true, "Quảng Bình", "0777888999", "74 Nguyễn Tất Thành", 9
                }
                );
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    1, "Nguyễn Thị Trà My", "2000-02-01", false, "0777887399", "213 Nguyễn Tất Thành", 1
                }
                );
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    2, "Nguyễn Thị Nở", "2000-02-16", false, "0777887333", "213 Nguyễn Sinh Cung", 2
                }
                );
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    3, "Trần Văn Cường", "1999-03-05", true, "0317263111", "773 Lý Thường Kiệt", 4
                }
                );
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    4, "Trần Thị Mỹ", "1999-05-05", false, "0317263111", "773 Lý Thường Kiệt", 5
                }
                );
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    5, "Trần Văn Cường", "1999-03-05", true, "0312963111", "773 Lý Bí", 6
                }
                );
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    6, "Lê Thị Mộng Mơ", "1986-09-05", false, "0333111890", "1 Tôn Đức Thắng", 7
                }
                );
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Birthday", "Gender", "TelephoneNumber", "Address", "AccountId" },
                values: new object[]
                {
                    7, "Trương Thị Ánh", "2004-12-13", false, "0787502603", "123 Bà Triệu", 10
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
