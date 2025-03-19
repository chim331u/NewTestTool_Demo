using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestToolApi.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectCode = table.Column<string>(type: "TEXT", nullable: true),
                    RmsNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PassPercent = table.Column<int>(type: "INTEGER", nullable: false),
                    TestCaseNum = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestSuites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    SuiteId = table.Column<int>(type: "INTEGER", nullable: false),
                    JiraReference = table.Column<string>(type: "TEXT", nullable: true),
                    Cr3atReference = table.Column<string>(type: "TEXT", nullable: true),
                    RequirementName = table.Column<string>(type: "TEXT", nullable: true),
                    RequirementDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeDescription = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSuites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSuites_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestSuiteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestCaseId = table.Column<string>(type: "TEXT", nullable: true),
                    TestCaseName = table.Column<string>(type: "TEXT", nullable: true),
                    TestCaseDescription = table.Column<string>(type: "TEXT", nullable: true),
                    TestCasePrecondition = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeDescription = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCases_TestSuites_TestSuiteId",
                        column: x => x.TestSuiteId,
                        principalTable: "TestSuites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestScripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestCaseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScriptStepNum = table.Column<string>(type: "TEXT", nullable: true),
                    ScriptStepDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ScriptExpectedResult = table.Column<string>(type: "TEXT", nullable: true),
                    ScriptResult = table.Column<int>(type: "INTEGER", nullable: true),
                    ScriptEnvironment = table.Column<int>(type: "INTEGER", nullable: true),
                    ScriptCompletedPercent = table.Column<double>(type: "REAL", nullable: false),
                    ScriptReferToTransaction = table.Column<string>(type: "TEXT", nullable: true),
                    ScriptRowMessage = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestScripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestScripts_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_TestSuiteId",
                table: "TestCases",
                column: "TestSuiteId");

            migrationBuilder.CreateIndex(
                name: "IX_TestScripts_TestCaseId",
                table: "TestScripts",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSuites_ProjectId",
                table: "TestSuites",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestScripts");

            migrationBuilder.DropTable(
                name: "TestCases");

            migrationBuilder.DropTable(
                name: "TestSuites");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
