using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    BranchManager = table.Column<string>(type: "TEXT", nullable: false),
                    EmployeeCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBranches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CivilId = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    WorkingBranchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_BankBranches_WorkingBranchId",
                        column: x => x.WorkingBranchId,
                        principalTable: "BankBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkingBranchId",
                table: "Employees",
                column: "WorkingBranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "BankBranches");
        }
    }
}
