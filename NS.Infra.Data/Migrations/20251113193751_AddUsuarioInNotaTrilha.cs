using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NS.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioInNotaTrilha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ID_USUARIO_FINAL",
                table: "NS_NOTA_TRILHA",
                type: "NUMBER(19)",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_NS_NOTA_TRILHA_ID_USUARIO_FINAL",
                table: "NS_NOTA_TRILHA",
                column: "ID_USUARIO_FINAL");

            migrationBuilder.AddForeignKey(
                name: "FK_NS_NOTA_TRILHA_NS_USUARIO_FINAL_ID_USUARIO_FINAL",
                table: "NS_NOTA_TRILHA",
                column: "ID_USUARIO_FINAL",
                principalTable: "NS_USUARIO_FINAL",
                principalColumn: "ID_USUARIO_FINAL",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NS_NOTA_TRILHA_NS_USUARIO_FINAL_ID_USUARIO_FINAL",
                table: "NS_NOTA_TRILHA");

            migrationBuilder.DropIndex(
                name: "IX_NS_NOTA_TRILHA_ID_USUARIO_FINAL",
                table: "NS_NOTA_TRILHA");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO_FINAL",
                table: "NS_NOTA_TRILHA");
        }
    }
}
