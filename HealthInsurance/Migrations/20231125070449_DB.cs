using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthInsurance.Migrations
{
    public partial class DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "BenefitDetail",
                columns: table => new
                {
                    BenefitDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitDetail", x => x.BenefitDetailId);
                    table.ForeignKey(
                        name: "FK_BenefitDetail_InsuranceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "InsuranceProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseList",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lv1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lv2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lv3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseList", x => x.DiseaseId);
                    table.ForeignKey(
                        name: "FK_DiseaseList_InsuranceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "InsuranceProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutstandingBenefit",
                columns: table => new
                {
                    ObId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    OdDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutstandingBenefit", x => x.ObId);
                    table.ForeignKey(
                        name: "FK_OutstandingBenefit_InsuranceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "InsuranceProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipationInformation",
                columns: table => new
                {
                    ParticipationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PIName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    PIDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipationInformation", x => x.ParticipationId);
                    table.ForeignKey(
                        name: "FK_ParticipationInformation_InsuranceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "InsuranceProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenefitDetail_ProductId",
                table: "BenefitDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseList_ProductId",
                table: "DiseaseList",
                column: "ProductId");

            

            migrationBuilder.CreateIndex(
                name: "IX_OutstandingBenefit_ProductId",
                table: "OutstandingBenefit",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationInformation_ProductId",
                table: "ParticipationInformation",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenefitDetail");

            migrationBuilder.DropTable(
                name: "DiseaseList");

            migrationBuilder.DropTable(
                name: "OutstandingBenefit");

            migrationBuilder.DropTable(
                name: "ParticipationInformation");

            
        }
    }
}
