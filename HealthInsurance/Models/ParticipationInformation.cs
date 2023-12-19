using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HealthInsurance.Models
{
    public class ParticipationInformation
    {
        [Key]
        public int ParticipationId { get; set; }
        [Required]
        public string PIName { get; set; }
        [Required]
        public string PIDescription { get; set; }
        [Required]
        public string File { get; set; }

        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual InsuranceProducts InsuranceProducts { get; set; }

    }
}
