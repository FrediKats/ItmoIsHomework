using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewYourself.WebApi.Migrations
{
    public partial class NewJwtAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AuthorizeDatas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AuthorizeDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    AccessToken = table.Column<string>(nullable: false),
                    PeerReviewUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => new { x.UserId, x.AccessToken });
                    table.ForeignKey(
                        name: "FK_Tokens_Users_PeerReviewUserId",
                        column: x => x.PeerReviewUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_PeerReviewUserId",
                table: "Tokens",
                column: "PeerReviewUserId");
        }
    }
}
