using Microsoft.EntityFrameworkCore.Migrations;

namespace PhanTichChungKhoan.Infrastructure.Migrations
{
    public partial class addcompanyname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "PriceBoardTemp",
                type: "NVARCHAR(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "PriceBoard",
                type: "NVARCHAR(2000)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "PriceBoardTemp");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "PriceBoard");
        }
    }
}
