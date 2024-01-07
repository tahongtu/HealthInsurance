namespace HealthInsurance.Models
{
    public class DashBoardModel
    {
        public List<UserInformation> userInformation { get; set; }
        public List<PackageInsurance> packageInsurances { get; set; }
        public List<ContractInsurance> contractInsurances { get; set; }
    }
}
