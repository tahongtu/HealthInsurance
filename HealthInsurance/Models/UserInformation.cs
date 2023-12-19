using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HealthInsurance.Models
{
    public class UserInformation
    {
        [Key]
        public int InfId { get; set; }
        [Required]
        public string AvtImage { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string CardId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LicenseDate { get; set; }
        [Required]
        public string FrontImage { get; set; }
        [Required]
        public string BackImage { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
