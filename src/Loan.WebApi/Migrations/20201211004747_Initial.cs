using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Loan.WebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LOAN");

            migrationBuilder.CreateTable(
                name: "ITEM",
                schema: "LOAN",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DATACRIACAO = table.Column<DateTime>(nullable: false),
                    NOME = table.Column<string>(nullable: true),
                    TIPO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEM_ID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PESSOA",
                schema: "LOAN",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DATACRIACAO = table.Column<DateTime>(nullable: false),
                    NOME = table.Column<string>(nullable: true),
                    EMAIL = table.Column<string>(nullable: true),
                    TELEFONE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PESSOA_ID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EMPRESTIMO",
                schema: "LOAN",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DATACRIACAO = table.Column<DateTime>(nullable: false),
                    DATA = table.Column<DateTime>(nullable: false),
                    DEVOLUCAO = table.Column<DateTime>(nullable: true),
                    PessoaId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESTIMO_ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EMPRESTIMO_ITEM_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "LOAN",
                        principalTable: "ITEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EMPRESTIMO_PESSOA_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "LOAN",
                        principalTable: "PESSOA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESTIMO_ItemId",
                schema: "LOAN",
                table: "EMPRESTIMO",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESTIMO_PessoaId",
                schema: "LOAN",
                table: "EMPRESTIMO",
                column: "PessoaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPRESTIMO",
                schema: "LOAN");

            migrationBuilder.DropTable(
                name: "ITEM",
                schema: "LOAN");

            migrationBuilder.DropTable(
                name: "PESSOA",
                schema: "LOAN");
        }
    }
}
