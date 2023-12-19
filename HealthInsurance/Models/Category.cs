using System.ComponentModel.DataAnnotations;
namespace HealthInsurance.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        
        public string CategoryDesription { get; set; }

        [Required]
        public string CategoryImage { get; set; }

        public ICollection<InsuranceProducts> InsuranceProducts { get; set; }

        
    }
}
