using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TADA.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] {"Id", "Name"},
                values: new object[]
                {
                    1, "Danh mục 1"
                }
                );

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] {"Id", "Name"},
                values: new object[]
                {
                    2, "Danh mục 2"
                }
                );

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] {"Id", "Name"},
                values: new object[]
                {
                    3, "Danh mục 3"
                }
                );

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Sale = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
