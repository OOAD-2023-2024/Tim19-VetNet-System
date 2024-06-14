using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetNet.Models
{
    public class Recept
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Datum i vrijeme")]
        public DateTime datumVrijeme { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Lijek")]
        public string lijek { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Range(1, int.MaxValue, ErrorMessage = "Neispravna vrijednost doze")]
        [Display(Name = "Doza")]
        public int doza { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Napomena")]
        public string napomena { get; set; }
        
        [ForeignKey("Ljubimac")]
        [Display(Name = "Ljubimac")]
        public int LjubimacId { get; set; }
        [Display(Name = "Ljubimac")]

        public Ljubimac? Ljubimac { get; set; }
        [ForeignKey("Korisnik")]
        [Display(Name = "Veterinar")]
        public string KorisnikId { get; set; }
        [Display(Name = "Veterinar")]

        public Korisnik? Korisnik { get; set; }
        public Recept() { }

    }
}
