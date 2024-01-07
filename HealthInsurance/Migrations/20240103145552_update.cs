using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthInsurance.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "ContractInsurance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "ContractInsurance");
        }
    }
}
