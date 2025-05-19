using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uni.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordsStudent");

            migrationBuilder.CreateTable(
                name: "RcordsStudent",
                columns: table => new
                {
                    RecordsrecordId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RcordsStudent", x => new { x.RecordsrecordId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_RcordsStudent_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RcordsStudent_Records_RecordsrecordId",
                        column: x => x.RecordsrecordId,
                        principalTable: "Records",
                        principalColumn: "recordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RcordsStudent_StudentId",
                table: "RcordsStudent",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RcordsStudent");

            migrationBuilder.CreateTable(
                name: "RecordsStudent",
                columns: table => new
                {
                    RecordsrecordId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordsStudent", x => new { x.RecordsrecordId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_RecordsStudent_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordsStudent_Records_RecordsrecordId",
                        column: x => x.RecordsrecordId,
                        principalTable: "Records",
                        principalColumn: "recordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordsStudent_StudentId",
                table: "RecordsStudent",
                column: "StudentId");
        }
    }
}
