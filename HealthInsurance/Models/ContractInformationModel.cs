namespace HealthInsurance.Models
{
    public class ContractInformationModel
    {
        public InsuranceProducts insuranceProducts { get; set; }
        public List<BenefitDetail> benefitDetails { get; set; }
        public List<DiseaseList> diseaseLists { get; set; }
        public List<OutstandingBenefit> outstandingBenefits { get; set; }
        public List<ParticipationInformation> participationInformation { get; set; }
        public PackageInsurance packageInsurances { get; set; }
        public User user { get; set; }
        public UserInformation userInformation { get; set; }
    }
}
