using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TADA.Migrations
{
    /// <inheritdoc />
    public partial class EditCustomerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TelephoneNumber",
                table: "Customers",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TelephoneNumber",
                table: "Customers",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);
        }
    }
}
