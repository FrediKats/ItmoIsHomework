using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewYourself.WebApi.Migrations
{
    public partial class FixNamingIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Users_PeerReviewUserId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Solutions_CourseSolutionId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CourseSolutionId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Participations_PeerReviewUserId",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "CourseSolutionId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PeerReviewUserId",
                table: "Participations");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SolutionId",
                table: "Reviews",
                column: "SolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_MemberId",
                table: "Participations",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Users_MemberId",
                table: "Participations",
                column: "MemberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Solutions_SolutionId",
                table: "Reviews",
                column: "SolutionId",
                principalTable: "Solutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Users_MemberId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Solutions_SolutionId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_SolutionId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Participations_MemberId",
                table: "Participations");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseSolutionId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PeerReviewUserId",
                table: "Participations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CourseSolutionId",
                table: "Reviews",
                column: "CourseSolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_PeerReviewUserId",
                table: "Participations",
                column: "PeerReviewUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Users_PeerReviewUserId",
                table: "Participations",
                column: "PeerReviewUserId",
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
        }
    }
}
