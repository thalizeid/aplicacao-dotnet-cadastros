using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistemaDivtech.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FORNECEDORES_DIVTECH",
                columns: table => new
                {
                    FornecedorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FornecedorNome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(14)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    Segmento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDORES_DIVTECH", x => x.FornecedorId);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTES_DIVTECH",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClienteNome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Foto = table.Column<byte[]>(type: "BLOB", nullable: true),
                    FornecedorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES_DIVTECH", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_CLIENTES_DIVTECH_FORNECEDORES_DIVTECH_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "FORNECEDORES_DIVTECH",
                        principalColumn: "FornecedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_DIVTECH_FornecedorId",
                table: "CLIENTES_DIVTECH",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTES_DIVTECH");

            migrationBuilder.DropTable(
                name: "FORNECEDORES_DIVTECH");
        }
    }
}
