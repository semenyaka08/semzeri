using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Semzeri.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEmailcolumninfotoUrlInfoentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "UrlInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "UrlInfos");
        }
    }
}
