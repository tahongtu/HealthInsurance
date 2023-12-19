using System.ComponentModel.DataAnnotations;

namespace HealthInsurance.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; } 

        public User()
        {
            this.Role = "Client";
        }

        public ICollection<UserInformation> UserInformation { get; set; }

    }

}
