using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ljubimci_korisnici_KorisnikId",
                table: "ljubimci");

            migrationBuilder.DropForeignKey(
                name: "FK_ObavjestenjeKorisnik_korisnici_KorisnikId",
                table: "ObavjestenjeKorisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_pregledi_korisnici_KorisnikId",
                table: "pregledi");

            migrationBuilder.DropForeignKey(
                name: "FK_recepti_korisnici_KorisnikId",
                table: "recepti");

            migrationBuilder.DropTable(
                name: "korisnici");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PoslovnicaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VeterinarskaSluzbaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adresa",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "datumRodjenja",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "prezime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "specijalizacija",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "spol",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PoslovnicaId",
                table: "AspNetUsers",
                column: "PoslovnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VeterinarskaSluzbaId",
                table: "AspNetUsers",
                column: "VeterinarskaSluzbaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_poslovnice_PoslovnicaId",
                table: "AspNetUsers",
                column: "PoslovnicaId",
                principalTable: "poslovnice",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_veterinarskeSluzbe_VeterinarskaSluzbaId",
                table: "AspNetUsers",
                column: "VeterinarskaSluzbaId",
                principalTable: "veterinarskeSluzbe",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ljubimci_AspNetUsers_KorisnikId",
                table: "ljubimci",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObavjestenjeKorisnik_AspNetUsers_KorisnikId",
                table: "ObavjestenjeKorisnik",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pregledi_AspNetUsers_KorisnikId",
                table: "pregledi",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recepti_AspNetUsers_KorisnikId",
                table: "recepti",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_poslovnice_PoslovnicaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_veterinarskeSluzbe_VeterinarskaSluzbaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ljubimci_AspNetUsers_KorisnikId",
                table: "ljubimci");

            migrationBuilder.DropForeignKey(
                name: "FK_ObavjestenjeKorisnik_AspNetUsers_KorisnikId",
                table: "ObavjestenjeKorisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_pregledi_AspNetUsers_KorisnikId",
                table: "pregledi");

            migrationBuilder.DropForeignKey(
                name: "FK_recepti_AspNetUsers_KorisnikId",
                table: "recepti");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PoslovnicaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VeterinarskaSluzbaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PoslovnicaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VeterinarskaSluzbaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "adresa",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "datumRodjenja",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "prezime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "specijalizacija",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "spol",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "korisnici",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PoslovnicaId = table.Column<int>(type: "int", nullable: false),
                    VeterinarskaSluzbaId = table.Column<int>(type: "int", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumRodjenja = table.Column<DateOnly>(type: "date", nullable: false),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spol = table.Column<int>(type: "int", nullable: false),
                    uloga = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_korisnici_PoslovnicaId",
                table: "korisnici",
                column: "PoslovnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_korisnici_VeterinarskaSluzbaId",
                table: "korisnici",
                column: "VeterinarskaSluzbaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ljubimci_korisnici_KorisnikId",
                table: "ljubimci",
                column: "KorisnikId",
                principalTable: "korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObavjestenjeKorisnik_korisnici_KorisnikId",
                table: "ObavjestenjeKorisnik",
                column: "KorisnikId",
                principalTable: "korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pregledi_korisnici_KorisnikId",
                table: "pregledi",
                column: "KorisnikId",
                principalTable: "korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recepti_korisnici_KorisnikId",
                table: "recepti",
                column: "KorisnikId",
                principalTable: "korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
