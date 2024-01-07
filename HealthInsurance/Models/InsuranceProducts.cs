using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsurance.Models
{
    public class InsuranceProducts
    {

        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Overview { get; set; }
        
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        

        public ICollection<BenefitDetail> BenefitDetail { get; set; }

        public ICollection<OutstandingBenefit> OutstandingBenefit { get; set; }

        public ICollection<DiseaseList> DiseaseList { get; set; }

        public ICollection<ParticipationInformation> ParticipationInformation { get; set; }

        public ICollection<PackageInsurance> PackageInsurances { get; set; }

        public ICollection<Document> Documents { get; set; }

        public ICollection<ContractInsurance> ContractInsurances { get; set; }

    }
}
