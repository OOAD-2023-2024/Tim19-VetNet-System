using System.ComponentModel.DataAnnotations;

namespace VetNet.Models
{
    public class Izvjestaj
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public DateTime datumVrijeme { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string sadrzaj { get; set; }
        public Izvjestaj() { }
    }
}
