using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsurance.Models
{
    public class ConsultMessage
    {
        [Key]
        public int ConsultId { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string InsuranceName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; } 

    }
}
