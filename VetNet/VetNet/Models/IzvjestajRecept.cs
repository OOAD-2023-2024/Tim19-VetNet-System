using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetNet.Models
{
    public class IzvjestajRecept
    {
        [Key]
        public int Id {  get; set; }
        [ForeignKey("Recept")]
        public int receptId { get; set; }
        [ForeignKey("Izvjestaj")]
        public int izvjestajId { get; set; }

        public Izvjestaj Izvjestaj { get; set; }
        public Recept Recept { get; set; }
        public IzvjestajRecept() { }
    }
}
