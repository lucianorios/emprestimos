using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Loan.WebApi.Migrations
{
    public partial class AddUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                schema: "LOAN",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DATACRIACAO = table.Column<DateTime>(nullable: false),
                    LOGIN = table.Column<string>(nullable: true),
                    PASSWORD = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_ID", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "LOAN",
                table: "USUARIO",
                columns: new[] { "ID", "DATACRIACAO", "LOGIN", "PASSWORD" },
                values: new object[] { 1L, new DateTime(2020, 12, 14, 9, 35, 35, 547, DateTimeKind.Local).AddTicks(498), "johndoe@loan.com", "3+8h2JwSRQMQPr8kUcATkg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO",
                schema: "LOAN");
        }
    }
}
