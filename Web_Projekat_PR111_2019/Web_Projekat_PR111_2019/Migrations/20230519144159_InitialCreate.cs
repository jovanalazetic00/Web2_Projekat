using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Projekat_PR111_2019.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administratori",
                columns: table => new
                {
                    KorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administratori", x => x.KorisnickoIme);
                });

            migrationBuilder.CreateTable(
                name: "Kupac",
                columns: table => new
                {
                    KorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusPorudzbine = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupac", x => x.KorisnickoIme);
                    table.ForeignKey(
                        name: "FK_Kupac_Administratori_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Administratori",
                        principalColumn: "KorisnickoIme");
                });

            migrationBuilder.CreateTable(
                name: "Prodavci",
                columns: table => new
                {
                    KorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdministratorKorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Verifikovan = table.Column<int>(type: "int", nullable: false),
                    StatusPoruzbine = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavci", x => x.KorisnickoIme);
                    table.ForeignKey(
                        name: "FK_Prodavci_Administratori_AdministratorKorisnickoIme",
                        column: x => x.AdministratorKorisnickoIme,
                        principalTable: "Administratori",
                        principalColumn: "KorisnickoIme");
                });

            migrationBuilder.CreateTable(
                name: "Artikal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    KolicinaArtikla = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IDProdavca = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikal_Prodavci_IDProdavca",
                        column: x => x.IDProdavca,
                        principalTable: "Prodavci",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikalId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KomentarPorudzbine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusPorudzbine = table.Column<int>(type: "int", nullable: false),
                    VrijemeDostave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijemePOrudzbine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDKupca = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProdavacKorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porudzbina_Artikal_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Porudzbina_Kupac_IDKupca",
                        column: x => x.IDKupca,
                        principalTable: "Kupac",
                        principalColumn: "KorisnickoIme");
                    table.ForeignKey(
                        name: "FK_Porudzbina_Prodavci_ProdavacKorisnickoIme",
                        column: x => x.ProdavacKorisnickoIme,
                        principalTable: "Prodavci",
                        principalColumn: "KorisnickoIme");
                });

            migrationBuilder.CreateTable(
                name: "ArtikliIPorudzbine",
                columns: table => new
                {
                    IDPorudzbine = table.Column<int>(type: "int", nullable: false),
                    IDArtikla = table.Column<int>(type: "int", nullable: false),
                    PorudzbinaId = table.Column<int>(type: "int", nullable: false),
                    ArtikalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KolicinaArtikla = table.Column<int>(type: "int", nullable: false),
                    CijenaPorudzbine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikliIPorudzbine", x => new { x.IDPorudzbine, x.IDArtikla });
                    table.ForeignKey(
                        name: "FK_ArtikliIPorudzbine_Artikal_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArtikliIPorudzbine_Porudzbina_PorudzbinaId",
                        column: x => x.PorudzbinaId,
                        principalTable: "Porudzbina",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administratori_KorisnickoIme",
                table: "Administratori",
                column: "KorisnickoIme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artikal_IDProdavca",
                table: "Artikal",
                column: "IDProdavca");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliIPorudzbine_ArtikalId",
                table: "ArtikliIPorudzbine",
                column: "ArtikalId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliIPorudzbine_PorudzbinaId",
                table: "ArtikliIPorudzbine",
                column: "PorudzbinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupac_KorisnickoIme",
                table: "Kupac",
                column: "KorisnickoIme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_ArtikalId",
                table: "Porudzbina",
                column: "ArtikalId");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_IDKupca",
                table: "Porudzbina",
                column: "IDKupca");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_ProdavacKorisnickoIme",
                table: "Porudzbina",
                column: "ProdavacKorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_Prodavci_AdministratorKorisnickoIme",
                table: "Prodavci",
                column: "AdministratorKorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_Prodavci_KorisnickoIme",
                table: "Prodavci",
                column: "KorisnickoIme",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtikliIPorudzbine");

            migrationBuilder.DropTable(
                name: "Porudzbina");

            migrationBuilder.DropTable(
                name: "Artikal");

            migrationBuilder.DropTable(
                name: "Kupac");

            migrationBuilder.DropTable(
                name: "Prodavci");

            migrationBuilder.DropTable(
                name: "Administratori");
        }
    }
}
