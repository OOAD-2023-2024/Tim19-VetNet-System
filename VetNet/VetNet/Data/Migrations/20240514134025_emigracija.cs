using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class emigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "izvjestaji",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumVrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_izvjestaji", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "obavjestenja",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumVrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_obavjestenja", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "poslovnice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poslovnice", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "veterinarskeSluzbe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veterinarskeSluzbe", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "korisnici",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spol = table.Column<int>(type: "int", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumRodjenja = table.Column<DateOnly>(type: "date", nullable: false),
                    uloga = table.Column<int>(type: "int", nullable: false),
                    PoslovnicaId = table.Column<int>(type: "int", nullable: true),
                    VeterinarskaSluzbaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_korisnici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_korisnici_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_korisnici_poslovnice_PoslovnicaId",
                        column: x => x.PoslovnicaId,
                        principalTable: "poslovnice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_korisnici_veterinarskeSluzbe_VeterinarskaSluzbaId",
                        column: x => x.VeterinarskaSluzbaId,
                        principalTable: "veterinarskeSluzbe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ljubimci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumRodjenja = table.Column<DateOnly>(type: "date", nullable: false),
                    slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rasa = table.Column<int>(type: "int", nullable: false),
                    spol = table.Column<int>(type: "int", nullable: false),
                    qrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ljubimci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ljubimci_korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObavjestenjeKorisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ObavjestenjeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObavjestenjeKorisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObavjestenjeKorisnik_korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObavjestenjeKorisnik_obavjestenja_ObavjestenjeId",
                        column: x => x.ObavjestenjeId,
                        principalTable: "obavjestenja",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pregledi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumVrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    razlog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postupak = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dijagnoza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    terapija = table.Column<bool>(type: "bit", nullable: false),
                    LjubimacId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pregledi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pregledi_korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pregledi_ljubimci_LjubimacId",
                        column: x => x.LjubimacId,
                        principalTable: "ljubimci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "recepti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumVrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lijek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doza = table.Column<int>(type: "int", nullable: false),
                    napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LjubimacId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recepti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recepti_korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recepti_ljubimci_LjubimacId",
                        column: x => x.LjubimacId,
                        principalTable: "ljubimci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "izvjestajPregledi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pregledId = table.Column<int>(type: "int", nullable: false),
                    izvjestajId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_izvjestajPregledi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_izvjestajPregledi_izvjestaji_izvjestajId",
                        column: x => x.izvjestajId,
                        principalTable: "izvjestaji",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_izvjestajPregledi_pregledi_pregledId",
                        column: x => x.pregledId,
                        principalTable: "pregledi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "izvjestajRecepti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    receptId = table.Column<int>(type: "int", nullable: false),
                    izvjestajId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_izvjestajRecepti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_izvjestajRecepti_izvjestaji_izvjestajId",
                        column: x => x.izvjestajId,
                        principalTable: "izvjestaji",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_izvjestajRecepti_recepti_receptId",
                        column: x => x.receptId,
                        principalTable: "recepti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_izvjestajPregledi_izvjestajId",
                table: "izvjestajPregledi",
                column: "izvjestajId");

            migrationBuilder.CreateIndex(
                name: "IX_izvjestajPregledi_pregledId",
                table: "izvjestajPregledi",
                column: "pregledId");

            migrationBuilder.CreateIndex(
                name: "IX_izvjestajRecepti_izvjestajId",
                table: "izvjestajRecepti",
                column: "izvjestajId");

            migrationBuilder.CreateIndex(
                name: "IX_izvjestajRecepti_receptId",
                table: "izvjestajRecepti",
                column: "receptId");

            migrationBuilder.CreateIndex(
                name: "IX_korisnici_PoslovnicaId",
                table: "korisnici",
                column: "PoslovnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_korisnici_VeterinarskaSluzbaId",
                table: "korisnici",
                column: "VeterinarskaSluzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_ljubimci_KorisnikId",
                table: "ljubimci",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_ObavjestenjeKorisnik_KorisnikId",
                table: "ObavjestenjeKorisnik",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_ObavjestenjeKorisnik_ObavjestenjeId",
                table: "ObavjestenjeKorisnik",
                column: "ObavjestenjeId");

            migrationBuilder.CreateIndex(
                name: "IX_pregledi_KorisnikId",
                table: "pregledi",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_pregledi_LjubimacId",
                table: "pregledi",
                column: "LjubimacId");

            migrationBuilder.CreateIndex(
                name: "IX_recepti_KorisnikId",
                table: "recepti",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_recepti_LjubimacId",
                table: "recepti",
                column: "LjubimacId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "izvjestajPregledi");

            migrationBuilder.DropTable(
                name: "izvjestajRecepti");

            migrationBuilder.DropTable(
                name: "ObavjestenjeKorisnik");

            migrationBuilder.DropTable(
                name: "pregledi");

            migrationBuilder.DropTable(
                name: "izvjestaji");

            migrationBuilder.DropTable(
                name: "recepti");

            migrationBuilder.DropTable(
                name: "obavjestenja");

            migrationBuilder.DropTable(
                name: "ljubimci");

            migrationBuilder.DropTable(
                name: "korisnici");

            migrationBuilder.DropTable(
                name: "poslovnice");

            migrationBuilder.DropTable(
                name: "veterinarskeSluzbe");
        }
    }
}
