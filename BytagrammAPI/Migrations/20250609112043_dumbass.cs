using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BytagrammAPI.Migrations
{
    /// <inheritdoc />
    public partial class dumbass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Communities",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Communities",
                newName: "Name");
        }
    }
}
