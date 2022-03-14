using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BadgerLogService.Data.Migrations
{
    public partial class create_logs_and_applications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    application_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    source = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    data = table.Column<string>(type: "VARCHAR(5000)", nullable: false),
                    stack_trace = table.Column<string>(type: "VARCHAR(5000)", nullable: false),
                    tags = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    level = table.Column<int>(type: "numeric(1,0)", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_logs", x => x.id);
                    table.ForeignKey(
                        name: "fk_logs_applications",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_logs_application_id",
                table: "logs",
                column: "application_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "applications");
        }
    }
}
