using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HealthInsurance.Models
{
    public class DiseaseList
    {
        [Key]
        public int DiseaseId { get; set; }
        [Required]
        public string DiseaseName { get; set; }
        [Required]
        public string Lv1 { get; set; }
        [Required]
        public string Lv2 { get; set; }
        [Required]
        public string Lv3 { get; set; }

        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual InsuranceProducts InsuranceProducts { get; set; }

    }
}
