using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewYourself.WebApi.Migrations
{
    public partial class PartitionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberTypes_Courses_CourseId",
                table: "MemberTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberTypes_UsersTable_UserId",
                table: "MemberTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberTypes",
                table: "MemberTypes");

            migrationBuilder.RenameTable(
                name: "MemberTypes",
                newName: "Participations");

            migrationBuilder.RenameIndex(
                name: "IX_MemberTypes_UserId",
                table: "Participations",
                newName: "IX_Participations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participations",
                table: "Participations",
                columns: new[] { "CourseId", "MemberId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Courses_CourseId",
                table: "Participations",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_UsersTable_UserId",
                table: "Participations",
                column: "UserId",
                principalTable: "UsersTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Courses_CourseId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_UsersTable_UserId",
                table: "Participations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participations",
                table: "Participations");

            migrationBuilder.RenameTable(
                name: "Participations",
                newName: "MemberTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_UserId",
                table: "MemberTypes",
                newName: "IX_MemberTypes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberTypes",
                table: "MemberTypes",
                columns: new[] { "CourseId", "MemberId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MemberTypes_Courses_CourseId",
                table: "MemberTypes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberTypes_UsersTable_UserId",
                table: "MemberTypes",
                column: "UserId",
                principalTable: "UsersTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
