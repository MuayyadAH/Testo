using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestoMVC.Migrations
{
    /// <inheritdoc />
    public partial class aa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_CaseStudies_CaseStudyId",
                table: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_TestResults_CaseStudyId",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "CaseStudyId",
                table: "TestResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaseStudyId",
                table: "TestResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_CaseStudyId",
                table: "TestResults",
                column: "CaseStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_CaseStudies_CaseStudyId",
                table: "TestResults",
                column: "CaseStudyId",
                principalTable: "CaseStudies",
                principalColumn: "CaseStudyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
