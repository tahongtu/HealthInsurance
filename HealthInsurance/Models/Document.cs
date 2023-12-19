using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HealthInsurance.Models
{
    public class Document
    {
        [Key]
        public int DocId { get; set; }
        [Required]
        public string DocName { get; set; }
        [Required]
        public string DocFile { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public InsuranceProducts InsuranceProducts { get; set; }
    }
}
