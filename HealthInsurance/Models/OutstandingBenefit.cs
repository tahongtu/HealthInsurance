using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HealthInsurance.Models
{
    public class OutstandingBenefit
    {
        [Key]
        public int ObId { get; set; }

        [Required]
        public string OdName { get; set; }
        [Required]
        public string OdDescription { get; set; }

        [Required]
        public string Image { get; set; }

        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual InsuranceProducts InsuranceProducts { get; set; }
    }
}
