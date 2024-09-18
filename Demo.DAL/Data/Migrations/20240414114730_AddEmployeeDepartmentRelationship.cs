using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.DAL.Data.Migrations
{
    public partial class AddEmployeeDepartmentRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Employee",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Employee",
                table: "Employees",
                column: "Employee");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Employee",
                table: "Employees",
                column: "Employee",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Employee",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Employee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Employee",
                table: "Employees");
        }
    }
}
