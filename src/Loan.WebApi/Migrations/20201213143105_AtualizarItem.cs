using Microsoft.EntityFrameworkCore.Migrations;

namespace Loan.WebApi.Migrations
{
    public partial class AtualizarItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CEDIDO",
                schema: "LOAN",
                table: "ITEM",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEDIDO",
                schema: "LOAN",
                table: "ITEM");
        }
    }
}
