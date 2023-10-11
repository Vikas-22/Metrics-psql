using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metrices_psql.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentString",
                table: "Employes");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Employes",
                newName: "employee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employee",
                table: "Employes",
                newName: "Department");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentString",
                table: "Employes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
