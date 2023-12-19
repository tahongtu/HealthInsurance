using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsurance.Models
{
    public class PackageInsurance
    {
        [Key]
        public int PackageId { get; set; }
        [Required]
        public string PackageName { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Limit { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual InsuranceProducts InsuranceProducts { get; set; }

    }
}
