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
                name: "Korisnici",
                columns: table => new
                {
                    IdKorisnika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotvrdaLozinke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StatusVerifrikacije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obrisan = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PotvrdaRegistracije = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Verifikovan = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.IdKorisnika);
                });

            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    ArtikalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    KolicinaArtikla = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Obrisan = table.Column<bool>(type: "bit", nullable: false),
                    IdKorisnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.ArtikalId);
                    table.ForeignKey(
                        name: "FK_Artikli_Korisnici_IdKorisnika",
                        column: x => x.IdKorisnika,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnika",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    IdPorudzbine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresaIsporuke = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    KomentarPorudzbine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CijenaPorudzbine = table.Column<double>(type: "float", nullable: false),
                    StatusPorudzbine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrijemeIsporuke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijemePorudzbine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdKorisnika = table.Column<int>(type: "int", nullable: false),
                    Obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.IdPorudzbine);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Korisnici_IdKorisnika",
                        column: x => x.IdKorisnika,
                        principalTable: "Korisnici",
                        principalColumn: "IdKorisnika",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtikliIPorudzbine",
                columns: table => new
                {
                    IDPorudzbine = table.Column<int>(type: "int", nullable: false),
                    IDArtikla = table.Column<int>(type: "int", nullable: false),
                    KolicinaArtikla = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikliIPorudzbine", x => new { x.IDPorudzbine, x.IDArtikla });
                    table.ForeignKey(
                        name: "FK_ArtikliIPorudzbine_Artikli_IDArtikla",
                        column: x => x.IDArtikla,
                        principalTable: "Artikli",
                        principalColumn: "ArtikalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtikliIPorudzbine_Porudzbine_IDPorudzbine",
                        column: x => x.IDPorudzbine,
                        principalTable: "Porudzbine",
                        principalColumn: "IdPorudzbine",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_IdKorisnika",
                table: "Artikli",
                column: "IdKorisnika");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliIPorudzbine_IDArtikla",
                table: "ArtikliIPorudzbine",
                column: "IDArtikla");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_Email",
                table: "Korisnici",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_KorisnickoIme",
                table: "Korisnici",
                column: "KorisnickoIme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_IdKorisnika",
                table: "Porudzbine",
                column: "IdKorisnika");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtikliIPorudzbine");

            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
