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
        [Display(Name = "Ime")]
        public string ime { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Datum rođenja")]
        public DateOnly datumRodjenja { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Slika")]
        public string slika { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Rasa")]
        public Rasa rasa { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Spol")]
        public Spol spol { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "QR Code")]
        public string qrCode { get; set; }
        [ForeignKey("Korisnik")]
        [Display(Name = "VlasnikID")]
        public string KorisnikId { get; set; }
        [Display(Name = "Vlasnik")]
        public Korisnik? Korisnik { get; set; }
        public Ljubimac() { }
    }
}
