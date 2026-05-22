using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conquistas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Icone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Regra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conquistas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    DiasParaLiberar = table.Column<int>(type: "int", nullable: true),
                    PrazoEmDias = table.Column<int>(type: "int", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PossuiForm = table.Column<bool>(type: "bit", nullable: false),
                    FormUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressosProfissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConquistasDesbloqueadas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressosProfissionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressoModulos",
                columns: table => new
                {
                    ModuloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgressoProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressoModulos", x => x.ModuloId);
                    table.ForeignKey(
                        name: "FK_ProgressoModulos_ProgressosProfissionais_ProgressoProfissionalId",
                        column: x => x.ProgressoProfissionalId,
                        principalTable: "ProgressosProfissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conquistas_Ordem",
                table: "Conquistas",
                column: "Ordem");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Ordem",
                table: "Modulos",
                column: "Ordem");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressoModulos_ProgressoProfissionalId",
                table: "ProgressoModulos",
                column: "ProgressoProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressosProfissionais_ProfissionalId",
                table: "ProgressosProfissionais",
                column: "ProfissionalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conquistas");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "ProgressoModulos");

            migrationBuilder.DropTable(
                name: "ProgressosProfissionais");
        }
    }
}
