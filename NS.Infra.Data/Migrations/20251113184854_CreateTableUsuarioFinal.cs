using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NS.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUsuarioFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NS_USUARIO_FINAL",
                columns: table => new
                {
                    ID_USUARIO_FINAL = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "varchar2(150)", maxLength: 150, nullable: false),
                    EMAIL = table.Column<string>(type: "varchar2(150)", nullable: false),
                    SENHA = table.Column<string>(type: "varchar2(150)", nullable: false),
                    DATA_CADASTRO = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_USUARIO_FINAL", x => x.ID_USUARIO_FINAL);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NS_USUARIO_FINAL");
        }
    }
}
