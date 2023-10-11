using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metrices_psql.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employee",
                table: "Employes");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Employes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Employes");

            migrationBuilder.AddColumn<int>(
                name: "employee",
                table: "Employes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
