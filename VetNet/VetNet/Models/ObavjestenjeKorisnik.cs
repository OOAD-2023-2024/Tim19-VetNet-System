using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetNet.Models
{
    public class ObavjestenjeKorisnik
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
        [ForeignKey("Obavjestenje")]
        public int ObavjestenjeId { get; set; }

        public Obavjestenje Obavjestenje { get; set; }
        public ObavjestenjeKorisnik() { }
    }
}
