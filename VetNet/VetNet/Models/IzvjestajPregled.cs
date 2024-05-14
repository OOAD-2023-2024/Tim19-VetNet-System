using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VetNet.Models
{
    public class IzvjestajPregled
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Pregled")]
        public int pregledId { get; set; }
        [ForeignKey("Izvjestaj")]
        public int izvjestajId { get; set; }

        public Izvjestaj Izvjestaj { get; set; }
        public Pregled Pregled { get; set; }
        public IzvjestajPregled() { }
    }
}
