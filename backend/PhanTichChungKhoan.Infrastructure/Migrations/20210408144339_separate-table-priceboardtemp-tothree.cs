using Microsoft.EntityFrameworkCore.Migrations;

namespace PhanTichChungKhoan.Infrastructure.Migrations
{
    public partial class separatetablepriceboardtemptothree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceBoardTemp");

            migrationBuilder.CreateTable(
                name: "HnxPriceBoardTemp",
                columns: table => new
                {
                    Exchange = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    Symbol = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    Price = table.Column<double>(nullable: false),
                    CompanyName = table.Column<string>(type: "NVARCHAR(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HnxPriceBoardTemp", x => new { x.Exchange, x.Symbol });
                });

            migrationBuilder.CreateTable(
                name: "HosePriceBoardTemp",
                columns: table => new
                {
                    Exchange = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    Symbol = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    Price = table.Column<double>(nullable: false),
                    CompanyName = table.Column<string>(type: "NVARCHAR(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HosePriceBoardTemp", x => new { x.Exchange, x.Symbol });
                });

            migrationBuilder.CreateTable(
                name: "UpcomPriceBoardTemp",
                columns: table => new
                {
                    Exchange = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    Symbol = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    Price = table.Column<double>(nullable: false),
                    CompanyName = table.Column<string>(type: "NVARCHAR(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpcomPriceBoardTemp", x => new { x.Exchange, x.Symbol });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HnxPriceBoardTemp");

            migrationBuilder.DropTable(
                name: "HosePriceBoardTemp");

            migrationBuilder.DropTable(
                name: "UpcomPriceBoardTemp");

            migrationBuilder.CreateTable(
                name: "PriceBoardTemp",
                columns: table => new
                {
                    Exchange = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    Symbol = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    CompanyName = table.Column<string>(type: "NVARCHAR(2000)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceBoardTemp", x => new { x.Exchange, x.Symbol });
                });
        }
    }
}
