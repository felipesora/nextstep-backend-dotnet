using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NS.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableRespostaFormulario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NS_RESPOSTA_FORMULARIO",
                columns: table => new
                {
                    ID_RESPOSTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NIVEL_EXPERIENCIA = table.Column<string>(type: "varchar2(30)", nullable: false),
                    OBJETIVO_CARREIRA = table.Column<string>(type: "varchar2(30)", nullable: false),
                    AREA_INTERESSE_1 = table.Column<string>(type: "varchar2(30)", nullable: false),
                    AREA_INTERESSE_2 = table.Column<string>(type: "varchar2(30)", nullable: true),
                    AREA_INTERESSE_3 = table.Column<string>(type: "varchar2(30)", nullable: true),
                    TEMPO_ESTUDO_SEMANAL = table.Column<string>(type: "varchar2(30)", nullable: false),
                    HABILIDADES_EXISTENTES = table.Column<string>(type: "varchar2(400)", maxLength: 400, nullable: true),
                    ID_USUARIO_FINAL = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_RESPOSTA_FORMULARIO", x => x.ID_RESPOSTA);
                    table.ForeignKey(
                        name: "FK_NS_RESPOSTA_FORMULARIO_NS_USUARIO_FINAL_ID_USUARIO_FINAL",
                        column: x => x.ID_USUARIO_FINAL,
                        principalTable: "NS_USUARIO_FINAL",
                        principalColumn: "ID_USUARIO_FINAL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NS_RESPOSTA_FORMULARIO_ID_USUARIO_FINAL",
                table: "NS_RESPOSTA_FORMULARIO",
                column: "ID_USUARIO_FINAL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NS_RESPOSTA_FORMULARIO");
        }
    }
}
