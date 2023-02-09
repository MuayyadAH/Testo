using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestoAPI.Migrations
{
    /// <inheritdoc />
    public partial class testcases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_CaseStudies_CaseStudyId",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "TestCases",
                table: "CaseStudies");

            migrationBuilder.DropColumn(
                name: "isTestCaseDone",
                table: "CaseStudies");

            migrationBuilder.RenameTable(
                name: "Results",
                newName: "TestResults");

            migrationBuilder.RenameIndex(
                name: "IX_Results_CaseStudyId",
                table: "TestResults",
                newName: "IX_TestResults_CaseStudyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestResults",
                table: "TestResults",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_CaseStudies_CaseStudyId",
                table: "TestResults",
                column: "CaseStudyId",
                principalTable: "CaseStudies",
                principalColumn: "CaseStudyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_CaseStudies_CaseStudyId",
                table: "TestResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestResults",
                table: "TestResults");

            migrationBuilder.RenameTable(
                name: "TestResults",
                newName: "Results");

            migrationBuilder.RenameIndex(
                name: "IX_TestResults_CaseStudyId",
                table: "Results",
                newName: "IX_Results_CaseStudyId");

            migrationBuilder.AddColumn<string>(
                name: "TestCases",
                table: "CaseStudies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "isTestCaseDone",
                table: "CaseStudies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_CaseStudies_CaseStudyId",
                table: "Results",
                column: "CaseStudyId",
                principalTable: "CaseStudies",
                principalColumn: "CaseStudyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
