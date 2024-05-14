using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetNet.Models
{
    public class Recept
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public DateTime datumVrijeme { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string lijek { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Range(1, int.MaxValue, ErrorMessage = "Neispravna vrijednost doze")]
        public int doza { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string napomena { get; set; }
        
        [ForeignKey("Ljubimac")]
        public int LjubimacId { get; set; }

        public Ljubimac Ljubimac { get; set; }
        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
        public Recept() { }

    }
}
