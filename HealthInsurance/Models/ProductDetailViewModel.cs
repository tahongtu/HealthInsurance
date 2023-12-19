namespace HealthInsurance.Models
{
    public class ProductDetailViewModel
    {
        public InsuranceProducts insuranceProducts { get; set; }
        public List<BenefitDetail> benefitDetails { get; set; }
        public List<DiseaseList> diseaseLists { get; set; }
        public List<OutstandingBenefit> outstandingBenefits { get; set; }
        public List<ParticipationInformation> participationInformation { get; set; }
        public List<PackageInsurance> packageInsurances { get; set; }
    }
}
