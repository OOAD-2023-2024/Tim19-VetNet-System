using System.ComponentModel.DataAnnotations;

namespace VetNet.Models
{
    public class Poslovnica
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string naziv {  get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string adresa { get; set; }
        [Phone(ErrorMessage = "Neispravan broj telefona")]
        [Required(ErrorMessage = "Obavezna vrijednost")]
        public string brojTelefona { get; set; }
        [Required(ErrorMessage = "Obavezna vrijednost")]
        [EmailAddress(ErrorMessage = "Neispravna email adresa")]
        public string email {  get; set; }

        public Poslovnica() { }
    }
}
