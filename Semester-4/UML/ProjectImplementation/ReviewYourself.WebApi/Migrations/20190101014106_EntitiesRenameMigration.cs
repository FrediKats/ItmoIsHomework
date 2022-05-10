using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewYourself.WebApi.Migrations
{
    public partial class EntitiesRenameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcing_UsersTable_AuthorId",
                table: "Announcing");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UsersTable_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTasks_UsersTable_AuthorId",
                table: "CourseTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_UsersTable_UserId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_UsersTable_AuthorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Solutions_SolutionId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_UsersTable_AuthorId",
                table: "Solutions");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_SolutionId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Participations",
                newName: "PeerReviewUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_UserId",
                table: "Participations",
                newName: "IX_Participations_PeerReviewUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "PeerReviewUserId",
                table: "Tokens",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseSolutionId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AuthorizeDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_PeerReviewUserId",
                table: "Tokens",
                column: "PeerReviewUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CourseSolutionId",
                table: "Reviews",
                column: "CourseSolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true,
                filter: "[Login] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcing_Users_AuthorId",
                table: "Announcing",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTasks_Users_AuthorId",
                table: "CourseTasks",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Users_PeerReviewUserId",
                table: "Participations",
                column: "PeerReviewUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Solutions_CourseSolutionId",
                table: "Reviews",
                column: "CourseSolutionId",
                principalTable: "Solutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Users_AuthorId",
                table: "Solutions",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_PeerReviewUserId",
                table: "Tokens",
                column: "PeerReviewUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcing_Users_AuthorId",
                table: "Announcing");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTasks_Users_AuthorId",
                table: "CourseTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Users_PeerReviewUserId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_AuthorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Solutions_CourseSolutionId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Users_AuthorId",
                table: "Solutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_PeerReviewUserId",
                table: "Tokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_PeerReviewUserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CourseSolutionId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PeerReviewUserId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "CourseSolutionId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AuthorizeDatas");

            migrationBuilder.RenameColumn(
                name: "PeerReviewUserId",
                table: "Participations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_PeerReviewUserId",
                table: "Participations",
                newName: "IX_Participations_UserId");

            migrationBuilder.CreateTable(
                name: "UsersTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Biography = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTable", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SolutionId",
                table: "Reviews",
                column: "SolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTable_Login",
                table: "UsersTable",
                column: "Login",
                unique: true,
                filter: "[Login] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcing_UsersTable_AuthorId",
                table: "Announcing",
                column: "AuthorId",
                principalTable: "UsersTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UsersTable_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "UsersTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTasks_UsersTable_AuthorId",
                table: "CourseTasks",
                column: "AuthorId",
                principalTable: "UsersTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_UsersTable_UserId",
                table: "Participations",
                column: "UserId",
                principalTable: "UsersTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_UsersTable_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "UsersTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Solutions_SolutionId",
                table: "Reviews",
                column: "SolutionId",
                principalTable: "Solutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_UsersTable_AuthorId",
                table: "Solutions",
                column: "AuthorId",
                principalTable: "UsersTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
