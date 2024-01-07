namespace HealthInsurance.Models
{
    public class ProfileModel
    {
        public UserInformation userInformation { get; set; }
        public List<PackageInsurance> packageInsurance { get; set; }
        //public List<ContractInsurance> contractInsurance { get; set; }
        public List<Policy> insurancePolicies { get; set; }
    }
}
