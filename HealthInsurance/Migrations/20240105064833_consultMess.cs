using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthInsurance.Migrations
{
    public partial class consultMess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultMessage",
                columns: table => new
                {
                    ConsultId = table.Column<int>(type: "int", nullable: false)
                                .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    InsuranceName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultMessage", x => x.ConsultId);
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
