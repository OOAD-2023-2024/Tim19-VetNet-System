using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetNet.Models
{
    public class Ljubimac
    {
        public enum Rasa { Labrador, Retriver, Ovcar, Pudla, Terijer, Pomeranac }
        
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string ime { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public DateOnly datumRodjenja { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string slika { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public Rasa rasa { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public Spol spol { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string qrCode { get; set; }
        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
        public Ljubimac() { }
    }
}
