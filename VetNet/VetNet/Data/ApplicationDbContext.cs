using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetNet.Models;

namespace VetNet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Ljubimac> Ljubimac { get; set; }
        public DbSet<Pregled> Pregled { get; set; }
        public DbSet<Izvjestaj> Izvjestaj { get; set; }
        public DbSet<Obavjestenje> Obavjestenje { get; set; }
        public DbSet<ObavjestenjeKorisnik> ObavjestenjeKorisnik { get; set; }
        public DbSet<Recept> Recept { get; set; }
        public DbSet<IzvjestajRecept> IzvjestajRecept { get; set; }
        public DbSet<IzvjestajPregled> IzvjestajPregled { get; set; }
        public DbSet<Poslovnica> Poslovnica { get; set; }
        public DbSet<VeterinarskaSluzba> VeterinarskaSluzba { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Korisnik>().ToTable("korisnici");
            builder.Entity<Korisnik>(b =>
            {
                b.Property(u => u.ime);
                b.Property(u => u.prezime);
                b.Property(u => u.spol);
                b.Property(u => u.adresa);
                b.Property(u => u.datumRodjenja);
                b.Property(u => u.specijalizacija);
                b.Property(u => u.PoslovnicaId);
                b.Property(u => u.VeterinarskaSluzbaId);
            });
            builder.Entity<Ljubimac>().ToTable("ljubimci");
            builder.Entity<Pregled>().ToTable("pregledi");
            builder.Entity<Izvjestaj>().ToTable("izvjestaji");
            builder.Entity<Obavjestenje>().ToTable("obavjestenja");
            builder.Entity<Recept>().ToTable("recepti");
            builder.Entity<IzvjestajRecept>().ToTable("izvjestajRecepti");
            builder.Entity<IzvjestajPregled>().ToTable("izvjestajPregledi");
            builder.Entity<Poslovnica>().ToTable("poslovnice");
            builder.Entity<VeterinarskaSluzba>().ToTable("veterinarskeSluzbe");

            base.OnModelCreating(builder);
        }

    }
}
