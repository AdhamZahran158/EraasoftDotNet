using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task13_v2.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsValidColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "otps",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "otps");
        }
    }
}
