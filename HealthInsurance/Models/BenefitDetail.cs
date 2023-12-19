using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HealthInsurance.Models
{
    public class BenefitDetail
    {
        [Key]
        public int BenefitDetailId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public string Image { get; set; }

        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual InsuranceProducts InsuranceProducts { get; set; }
    }
}
