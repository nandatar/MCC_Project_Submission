using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class initialdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "class_mcc",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_mcc", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projecttitle = table.Column<string>(name: "project_title", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uml = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    bpmn = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currentstatus = table.Column<int>(name: "current_status", type: "int", nullable: false),
                    score = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_status_current_status",
                        column: x => x.currentstatus,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectid = table.Column<int>(name: "project_id", type: "int", nullable: true),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    revision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusid = table.Column<int>(name: "status_id", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_history_project_project_id",
                        column: x => x.projectid,
                        principalTable: "project",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_history_status_status_id",
                        column: x => x.statusid,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "participant",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    batch = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    statusmcc = table.Column<int>(name: "status_mcc", type: "int", nullable: false),
                    projectid = table.Column<int>(name: "project_id", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participant", x => x.nik);
                    table.ForeignKey(
                        name: "FK_participant_project_project_id",
                        column: x => x.projectid,
                        principalTable: "project",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    classid = table.Column<int>(name: "class_id", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.nik);
                    table.ForeignKey(
                        name: "FK_employee_class_mcc_class_id",
                        column: x => x.classid,
                        principalTable: "class_mcc",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_employee_participant_nik",
                        column: x => x.nik,
                        principalTable: "participant",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.nik);
                    table.ForeignKey(
                        name: "FK_account_employee_nik",
                        column: x => x.nik,
                        principalTable: "employee",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_class_id",
                table: "employee",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_project_id",
                table: "history",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_status_id",
                table: "history",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_participant_project_id",
                table: "participant",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_current_status",
                table: "project",
                column: "current_status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "class_mcc");

            migrationBuilder.DropTable(
                name: "participant");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "status");
        }
    }
}
