using System.ComponentModel.DataAnnotations;

namespace VetNet.Models
{
    public class Izvjestaj
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Datum i vrijeme")]
        public DateTime datumVrijeme { get; set; }

        [Required(ErrorMessage = "Obavezna vrijednost")]
        [Display(Name = "Sadržaj")]
        public string sadrzaj { get; set; }
        public Izvjestaj() { }
    }
}
