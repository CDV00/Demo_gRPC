using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrpcService2.Infrastructure.Migrations
{
    public partial class AddFieldInventoryInTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "Products",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Products");
        }
    }
}
