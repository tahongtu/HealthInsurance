using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HealthInsurance.Models
{
    public class ContractInsurance
    {
        [Key]
        public int ContractId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

        [Required]
        public int PackageId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual InsuranceProducts InsuranceProducts { get; set; }

        //public int PackageId { get; set; }

        //[ForeignKey("PackageId")]
        //public virtual PackageInsurance PackageInsurance { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


    }
}
