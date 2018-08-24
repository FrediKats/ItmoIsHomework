using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewYourself.WebApi.Migrations
{
    public partial class InitializeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Courses",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Courses", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Members", x => x.Id); });

            migrationBuilder.CreateTable(
                "Announcing",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PostTime = table.Column<DateTime>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcing", x => x.Id);
                    table.ForeignKey(
                        "FK_Announcing_Members_AuthorId",
                        x => x.AuthorId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Announcing_Courses_CourseId",
                        x => x.CourseId,
                        "Courses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "CourseTasks",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PostTime = table.Column<DateTime>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTasks", x => x.Id);
                    table.ForeignKey(
                        "FK_CourseTasks_Members_AuthorId",
                        x => x.AuthorId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_CourseTasks_Courses_CourseId",
                        x => x.CourseId,
                        "Courses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "MemberTypes",
                table => new
                {
                    MemberId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    CourseId = table.Column<Guid>(nullable: false),
                    Permission = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTypes", x => new {x.CourseId, x.MemberId});
                    table.ForeignKey(
                        "FK_MemberTypes_Courses_CourseId",
                        x => x.CourseId,
                        "Courses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_MemberTypes_Members_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Comments",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    PostTime = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    AnnouncingId = table.Column<Guid>(nullable: true),
                    CourseTaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        "FK_Comments_Announcing_AnnouncingId",
                        x => x.AnnouncingId,
                        "Announcing",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Comments_Members_AuthorId",
                        x => x.AuthorId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Comments_CourseTasks_CourseTaskId",
                        x => x.CourseTaskId,
                        "CourseTasks",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Criterias",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MaxPoint = table.Column<int>(nullable: false),
                    CourseTaskId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterias", x => x.Id);
                    table.ForeignKey(
                        "FK_Criterias_CourseTasks_CourseTaskId",
                        x => x.CourseTaskId,
                        "CourseTasks",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Solutions",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TextData = table.Column<string>(nullable: true),
                    PostTime = table.Column<DateTime>(nullable: false),
                    IsResolved = table.Column<bool>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    CourseTaskId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.Id);
                    table.ForeignKey(
                        "FK_Solutions_Members_AuthorId",
                        x => x.AuthorId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Solutions_CourseTasks_CourseTaskId",
                        x => x.CourseTaskId,
                        "CourseTasks",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Reviews",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PostTime = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    SolutionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        "FK_Reviews_Members_AuthorId",
                        x => x.AuthorId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Reviews_Solutions_SolutionId",
                        x => x.SolutionId,
                        "Solutions",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "ReviewCriterias",
                table => new
                {
                    ReviewId = table.Column<Guid>(nullable: false),
                    CriteriaId = table.Column<Guid>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewCriterias", x => new {x.CriteriaId, x.ReviewId});
                    table.ForeignKey(
                        "FK_ReviewCriterias_Criterias_CriteriaId",
                        x => x.CriteriaId,
                        "Criterias",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_ReviewCriterias_Reviews_ReviewId",
                        x => x.ReviewId,
                        "Reviews",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Announcing_AuthorId",
                "Announcing",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_Announcing_CourseId",
                "Announcing",
                "CourseId");

            migrationBuilder.CreateIndex(
                "IX_Comments_AnnouncingId",
                "Comments",
                "AnnouncingId");

            migrationBuilder.CreateIndex(
                "IX_Comments_AuthorId",
                "Comments",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_Comments_CourseTaskId",
                "Comments",
                "CourseTaskId");

            migrationBuilder.CreateIndex(
                "IX_CourseTasks_AuthorId",
                "CourseTasks",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_CourseTasks_CourseId",
                "CourseTasks",
                "CourseId");

            migrationBuilder.CreateIndex(
                "IX_Criterias_CourseTaskId",
                "Criterias",
                "CourseTaskId");

            migrationBuilder.CreateIndex(
                "IX_MemberTypes_UserId",
                "MemberTypes",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_ReviewCriterias_ReviewId",
                "ReviewCriterias",
                "ReviewId");

            migrationBuilder.CreateIndex(
                "IX_Reviews_AuthorId",
                "Reviews",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_Reviews_SolutionId",
                "Reviews",
                "SolutionId");

            migrationBuilder.CreateIndex(
                "IX_Solutions_AuthorId",
                "Solutions",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_Solutions_CourseTaskId",
                "Solutions",
                "CourseTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Comments");

            migrationBuilder.DropTable(
                "MemberTypes");

            migrationBuilder.DropTable(
                "ReviewCriterias");

            migrationBuilder.DropTable(
                "Announcing");

            migrationBuilder.DropTable(
                "Criterias");

            migrationBuilder.DropTable(
                "Reviews");

            migrationBuilder.DropTable(
                "Solutions");

            migrationBuilder.DropTable(
                "CourseTasks");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Courses");
        }
    }
}