using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoredApi.Migrations
{
    public partial class addNameToGaAndJga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "JoinActivityRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GroupActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "JoinActivityRequests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GroupActivities");
        }
    }
}
