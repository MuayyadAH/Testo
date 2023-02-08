using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestoAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseStudies",
                columns: table => new
                {
                    CaseStudyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Questionnaires = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestCases = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isTestCaseDone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStudies", x => x.CaseStudyId);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeElapsed = table.Column<int>(type: "int", nullable: true),
                    AverageTimeOnTask = table.Column<float>(type: "real", nullable: true),
                    VisitedSites = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Errors = table.Column<int>(type: "int", nullable: true),
                    UserClicks = table.Column<int>(type: "int", nullable: true),
                    TaskSucessRate = table.Column<float>(type: "real", nullable: true),
                    ScrollDepthRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseStudyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_CaseStudies_CaseStudyId",
                        column: x => x.CaseStudyId,
                        principalTable: "CaseStudies",
                        principalColumn: "CaseStudyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_CaseStudyId",
                table: "Results",
                column: "CaseStudyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "CaseStudies");
        }
    }
}
