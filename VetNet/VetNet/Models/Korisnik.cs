using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace VetNet.Models
{
    public class Korisnik : IdentityUser
    {
        public enum Uloga { administrator, veterinar, apotekar,vlasnik};
        
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string ime { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string prezime { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public Spol spol { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string adresa { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public DateOnly datumRodjenja { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public Uloga uloga { get; set; }

        [ForeignKey("Poslovnica")]
        [AllowNull]
        public int PoslovnicaId { get; set; }

        public Poslovnica Poslovnica { get; set; }

        [ForeignKey("VeterinarskaSluzba")]
        [AllowNull]
        public int VeterinarskaSluzbaId { get; set; }

        public VeterinarskaSluzba VeterinarskaSluzba { get; set; }


    }
}
