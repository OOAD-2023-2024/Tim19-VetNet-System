using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetNet.Models
{
    public class Pregled
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public DateTime datumVrijeme { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string razlog { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string postupak { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string dijagnoza { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string napomena { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public Boolean terapija { get; set; }
        
        [ForeignKey("Ljubimac")]
        public int LjubimacId { get; set; }

        public Ljubimac Ljubimac { get; set; }

        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
        public Pregled() { }
    }
}
