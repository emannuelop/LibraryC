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
                    table.PrimaryKey("PK__autor__5FC3872DC301C732", x => x.id_autor);
                });

            migrationBuilder.CreateTable(
                name: "biblioteca",
                columns: table => new
                {
                    id_biblioteca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    endereco = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bibliote__1EEBBDFE281AC4B8", x => x.id_biblioteca);
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    telefone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    cpf = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cliente__677F38F5CA8CA5A3", x => x.id_cliente);
                });

            migrationBuilder.CreateTable(
                name: "livro",
                columns: table => new
                {
                    id_livro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ano_publicacao = table.Column<int>(type: "int", nullable: true),
                    id_autor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__livro__C252147DF73055FC", x => x.id_livro);
                    table.ForeignKey(
                        name: "FK__livro__id_autor__46E78A0C",
                        column: x => x.id_autor,
                        principalTable: "autor",
                        principalColumn: "id_autor");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    senha = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    perfil = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    cpf = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    id_biblioteca = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario__4E3E04AD8C3DEA94", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK__usuario__id_bibl__3B75D760",
                        column: x => x.id_biblioteca,
                        principalTable: "biblioteca",
                        principalColumn: "id_biblioteca");
                });

            migrationBuilder.CreateTable(
                name: "multa",
                columns: table => new
                {
                    id_multa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    motivo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__multa__295650BB36BC9AE5", x => x.id_multa);
                    table.ForeignKey(
                        name: "FK__multa__id_client__4222D4EF",
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
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_livro = table.Column<int>(type: "int", nullable: false),
                    data_emprestimo = table.Column<DateOnly>(type: "date", nullable: true),
                    data_devolucao = table.Column<DateOnly>(type: "date", nullable: true),
                    data_prevista_devolucao = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    id_biblioteca = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__empresti__45FD187E42C5A0B8", x => x.id_emprestimo);
                    table.ForeignKey(
                        name: "FK__emprestim__id_bi__4D94879B",
                        column: x => x.id_biblioteca,
                        principalTable: "biblioteca",
                        principalColumn: "id_biblioteca");
                    table.ForeignKey(
                        name: "FK__emprestim__id_cl__4E88ABD4",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "FK__emprestim__id_li__4F7CD00D",
                        column: x => x.id_livro,
                        principalTable: "livro",
                        principalColumn: "id_livro");
                });

            migrationBuilder.CreateTable(
                name: "livro_biblioteca",
                columns: table => new
                {
                    id_livro_biblioteca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_livro = table.Column<int>(type: "int", nullable: true),
                    id_biblioteca = table.Column<int>(type: "int", nullable: true),
                    quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__livro_bi__C7822DFEA5F88D41", x => x.id_livro_biblioteca);
                    table.ForeignKey(
                        name: "FK__livro_bib__id_bi__4AB81AF0",
                        column: x => x.id_biblioteca,
                        principalTable: "biblioteca",
                        principalColumn: "id_biblioteca");
                    table.ForeignKey(
                        name: "FK__livro_bib__id_li__49C3F6B7",
                        column: x => x.id_livro,
                        principalTable: "livro",
                        principalColumn: "id_livro");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__cliente__AB6E616407A7B8AF",
                table: "cliente",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__cliente__D836E71F90420DBC",
                table: "cliente",
                column: "cpf",
                unique: true,
                filter: "[cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_emprestimo_id_biblioteca",
                table: "emprestimo",
                column: "id_biblioteca");

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
                name: "IX_livro_biblioteca_id_biblioteca",
                table: "livro_biblioteca",
                column: "id_biblioteca");

            migrationBuilder.CreateIndex(
                name: "IX_livro_biblioteca_id_livro",
                table: "livro_biblioteca",
                column: "id_livro");

            migrationBuilder.CreateIndex(
                name: "IX_multa_id_cliente",
                table: "multa",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_id_biblioteca",
                table: "usuario",
                column: "id_biblioteca");

            migrationBuilder.CreateIndex(
                name: "UQ__usuario__AB6E6164A876FDE6",
                table: "usuario",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__usuario__D836E71F932DAC61",
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
                name: "livro_biblioteca");

            migrationBuilder.DropTable(
                name: "multa");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "livro");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "biblioteca");

            migrationBuilder.DropTable(
                name: "autor");
        }
    }
}
