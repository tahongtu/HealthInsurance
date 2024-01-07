using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace HealthInsurance.Models
{



    public class Policy
    {
        [Parameter("uint256", "userId", 1)]
        public Int64 UserId { get; set; }

        [Parameter("string", "fullName", 2)]
        public string FullName { get; set; }

        [Parameter("string", "phoneNumber", 3)]
        public string PhoneNumber { get; set; }

        [Parameter("string", "email", 4)]
        public string Email { get; set; }

        [Parameter("string", "idCard", 5)]
        public string IdCard { get; set; }

        [Parameter("string", "productName", 6)]
        public string ProductName { get; set; }

        [Parameter("string", "packageName", 7)]
        public string PackageName { get; set; }

        [Parameter("string", "benefitDetail", 8)]
        public string BenefitDetail { get; set; }

        [Parameter("string", "price", 9)]
        public string Price { get; set; }

        [Parameter("string", "startDate", 10)]
        public string StartDate { get; set; }

        [Parameter("string", "endDate", 11)]
        public string EndDate { get; set; }
    }

    public class PolicyService
    {
        private readonly Contract contract;
        private readonly Web3 web3;

        public PolicyService(string contractAddress, string providerUrl)
        {
            web3 = new Web3(providerUrl);
            string abiJson = System.IO.File.ReadAllText("../HealthInsurance/wwwroot/abi/InsuranceContract.json");
            contract = web3.Eth.GetContract(abiJson, contractAddress);
        }

        public async Task<List<Policy>> GetAllPolicies()
        {
            var function = contract.GetFunction("getAllPolicies");
            var policies = await function.CallAsync<List<Policy>>();
            return policies;
        }

        
    }
}
