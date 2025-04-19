using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stefanini.Infra.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Name", "UF" },
                values: new object[,]
                {
                    { 1, "São Paulo", "SP" },
                    { 2, "Rio de Janeiro", "RJ" },
                    { 3, "Belo Horizonte", "MG" },
                    { 4, "Salvador", "BA" },
                    { 5, "Porto Alegre", "RS" },
                    { 6, "Curitiba", "PR" },
                    { 7, "Fortaleza", "CE" },
                    { 8, "Recife", "PE" },
                    { 9, "Manaus", "AM" },
                    { 10, "Belém", "PA" },
                    { 11, "Goiânia", "GO" },
                    { 12, "Campo Grande", "MS" },
                    { 13, "Teresina", "PI" },
                    { 14, "São Luís", "MA" },
                    { 15, "João Pessoa", "PB" },
                    { 16, "Natal", "RN" },
                    { 17, "Maceió", "AL" }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "CityId", "CPF", "Name", "Age" },
                values: new object[,]
                {
                    { 1, 1, "12345678901", "John Doe", 30 },
                    { 2, 2, "23456789012", "Jane Smith", 25 },
                    { 3, 3, "34567890123", "Alice Johnson", 28 },
                    { 4, 4, "45678901234", "Bob Brown", 35 },
                    { 5, 5, "56789012345", "Charlie Davis", 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_Name",
                table: "City",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_CityId",
                table: "Person",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CPF",
                table: "Person",
                column: "CPF",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
