using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestoAPI.Migrations
{
    /// <inheritdoc />
    public partial class testcases2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestsCases",
                columns: table => new
                {
                    TestCaseNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestCaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestCaseLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isTestCaseDone = table.Column<bool>(type: "bit", nullable: true),
                    CaseStudyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsCases", x => x.TestCaseNumber);
                    table.ForeignKey(
                        name: "FK_TestsCases_CaseStudies_CaseStudyId",
                        column: x => x.CaseStudyId,
                        principalTable: "CaseStudies",
                        principalColumn: "CaseStudyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestsCases_CaseStudyId",
                table: "TestsCases",
                column: "CaseStudyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestsCases");
        }
    }
}
