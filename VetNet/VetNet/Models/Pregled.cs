using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetNet.Models
{
    public class Pregled
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Datum i vrijeme")]
        public DateTime datumVrijeme { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Razlog")]
        public string razlog { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Postupak")]
        public string postupak { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Dijagnoza")]
        public string dijagnoza { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Napomena")]
        public string napomena { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Terapija")]
        public Boolean terapija { get; set; }
        
        [ForeignKey("Ljubimac")]
        [Display(Name = "Ljubimac")]
        public int LjubimacId { get; set; }
        [Display(Name = "Ljubimac")]

        public Ljubimac Ljubimac { get; set; }

        [ForeignKey("Korisnik")]
        [Display(Name = "Veterinar")]
        public string KorisnikId { get; set; }
        [Display(Name = "Veterinar")]

        public Korisnik Korisnik { get; set; }
        public Pregled() { }
    }
}
