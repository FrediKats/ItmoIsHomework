using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewYourself.WebApi.Migrations
{
    public partial class updatev1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Password",
                "Users");

            migrationBuilder.CreateTable(
                "AuthorizeDatas",
                table => new
                {
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AuthorizeDatas", x => x.Login); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AuthorizeDatas");

            migrationBuilder.AddColumn<string>(
                "Password",
                "Users",
                nullable: true);
        }
    }
}