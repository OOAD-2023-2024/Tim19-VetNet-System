using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VetNet.Models
{
    public class Korisnik : IdentityUser
    {
        public enum Specijalizacija {
            [Display(Name = "Klinička medicina malih životinja")]
            KlinickaMedicinaMalihZivotinja,
            [Display(Name = "Parazitologija")]
            Parazitologija,
            [Display(Name = "Sigurnost hrane")]
            SigurnostHrane,
            [Display(Name = "Teriogenelogija domaćih životinja")]
            TeriogenelogijaDomacihZivotinja,
            [Display(Name = "Veterinarska epidemiologija i ekonomika")]
            VeterinarskaEpidemiologijaIEkonomika,
            [Display(Name = "Zarazne bolesti životinja")]
            ZarazneBolestiZivotinja
        };

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string ime { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Prezime")]
        public string prezime { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Spol")]
        public Spol spol { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Adresa")]
        public string adresa { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Datum rođenja")]
        public DateOnly datumRodjenja { get; set; }
        [Phone(ErrorMessage = "Neispravan broj telefona")]
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Broj Telefona")]
        public string brojTelefona { get; set; }

        [AllowNull]
        [Display(Name = "Specijalizacija")]
        public Specijalizacija? specijalizacija { get; set; }

        [ForeignKey("Poslovnica")]
        [AllowNull]
        public int? PoslovnicaId { get; set; }

        public Poslovnica? Poslovnica { get; set; }

        [ForeignKey("VeterinarskaSluzba")]
        [AllowNull]
        public int? VeterinarskaSluzbaId { get; set; }

        public VeterinarskaSluzba? VeterinarskaSluzba { get; set; }


    }
}
