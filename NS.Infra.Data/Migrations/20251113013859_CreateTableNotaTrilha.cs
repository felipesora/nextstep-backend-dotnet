using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NS.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableNotaTrilha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NS_NOTA_TRILHA",
                columns: table => new
                {
                    ID_NOTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    VALOR_NOTA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "varchar2(400)", nullable: true),
                    ID_TRILHA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_NOTA_TRILHA", x => x.ID_NOTA);
                    table.ForeignKey(
                        name: "FK_NS_NOTA_TRILHA_NS_TRILHA_ID_TRILHA",
                        column: x => x.ID_TRILHA,
                        principalTable: "NS_TRILHA",
                        principalColumn: "ID_TRILHA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NS_NOTA_TRILHA_ID_TRILHA",
                table: "NS_NOTA_TRILHA",
                column: "ID_TRILHA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NS_NOTA_TRILHA");
        }
    }
}
