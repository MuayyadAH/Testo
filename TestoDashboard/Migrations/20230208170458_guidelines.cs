using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestoAPI.Migrations
{
    /// <inheritdoc />
    public partial class guidelines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuideLines",
                table: "CaseStudies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartingPoint",
                table: "CaseStudies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuideLines",
                table: "CaseStudies");

            migrationBuilder.DropColumn(
                name: "StartingPoint",
                table: "CaseStudies");
        }
    }
}
