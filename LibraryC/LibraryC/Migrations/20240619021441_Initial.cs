using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryC.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autor",
                columns: table => new
                {
                    id_autor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__autor__5FC3872D8D84111E", x => x.id_autor);
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    cpf = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    telefone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cliente__677F38F5ADCBD31A", x => x.id_cliente);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    cpf = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    senha = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    perfil = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario__4E3E04ADBB2183BB", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "livro",
                columns: table => new
                {
                    id_livro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ano_publicacao = table.Column<int>(type: "int", nullable: true),
                    id_autor = table.Column<int>(type: "int", nullable: true),
                    quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__livro__C252147D34AEE0DA", x => x.id_livro);
                    table.ForeignKey(
                        name: "FK__livro__id_autor__3D5E1FD2",
                        column: x => x.id_autor,
                        principalTable: "autor",
                        principalColumn: "id_autor");
                });

            migrationBuilder.CreateTable(
                name: "multa",
                columns: table => new
                {
                    id_multa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data = table.Column<DateOnly>(type: "date", nullable: true),
                    motivo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__multa__295650BB02D87DB9", x => x.id_multa);
                    table.ForeignKey(
                        name: "FK__multa__id_client__440B1D61",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                });

            migrationBuilder.CreateTable(
                name: "emprestimo",
                columns: table => new
                {
                    id_emprestimo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_emprestimo = table.Column<DateOnly>(type: "date", nullable: true),
                    data_prevista_devolucao = table.Column<DateOnly>(type: "date", nullable: true),
                    data_devolucao = table.Column<DateOnly>(type: "date", nullable: true),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_livro = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__empresti__45FD187EAB0C3519", x => x.id_emprestimo);
                    table.ForeignKey(
                        name: "FK__emprestim__id_cl__403A8C7D",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "FK__emprestim__id_li__412EB0B6",
                        column: x => x.id_livro,
                        principalTable: "livro",
                        principalColumn: "id_livro");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__cliente__AB6E6164D20DC6AE",
                table: "cliente",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__cliente__D836E71F47A837ED",
                table: "cliente",
                column: "cpf",
                unique: true,
                filter: "[cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_emprestimo_id_cliente",
                table: "emprestimo",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_emprestimo_id_livro",
                table: "emprestimo",
                column: "id_livro");

            migrationBuilder.CreateIndex(
                name: "IX_livro_id_autor",
                table: "livro",
                column: "id_autor");

            migrationBuilder.CreateIndex(
                name: "IX_multa_id_cliente",
                table: "multa",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "UQ__usuario__AB6E6164C431BFFF",
                table: "usuario",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__usuario__D836E71F34E4F432",
                table: "usuario",
                column: "cpf",
                unique: true,
                filter: "[cpf] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emprestimo");

            migrationBuilder.DropTable(
                name: "multa");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "livro");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "autor");
        }
    }
}
