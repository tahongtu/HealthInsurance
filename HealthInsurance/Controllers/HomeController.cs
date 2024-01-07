using HealthInsurance.Data;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using Nethereum.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nethereum.ABI;

namespace HealthInsurance.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly HealthInsuranceContext _context;

//        private Web3 web3 = new Web3("http://127.0.0.1:7545"); // Địa chỉ RPC endpoint của Ganache
//        string contractAddress = "0x8559583e53a934c10E0d77Ef143b4A3055e30A2f"; // Địa chỉ của smart contract đã triển khai trên Ganache

//        string abiJson = @"
//[
//    {
//      ""inputs"": [
//        {
//          ""internalType"": ""uint256"",
//          ""name"": """",
//          ""type"": ""uint256""
//        }
//      ],
//      ""name"": ""policies"",
//      ""outputs"": [
//        {
//          ""internalType"": ""uint256"",
//          ""name"": ""userId"",
//          ""type"": ""uint256""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""fullName"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""phoneNumber"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""email"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""idCard"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""productName"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""packageName"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""benefitDetail"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""price"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""startDate"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""endDate"",
//          ""type"": ""string""
//        }
//      ],
//      ""stateMutability"": ""view"",
//      ""type"": ""function""
//    },
//    {
//      ""inputs"": [
//        {
//          ""internalType"": ""uint256"",
//          ""name"": ""_userId"",
//          ""type"": ""uint256""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_fullName"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_phoneNumber"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_email"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_idCard"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_productName"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_packageName"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_benefitDetail"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_price"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_startDate"",
//          ""type"": ""string""
//        },
//        {
//          ""internalType"": ""string"",
//          ""name"": ""_endDate"",
//          ""type"": ""string""
//        }
//      ],
//      ""name"": ""createPolicy"",
//      ""outputs"": [],
//      ""stateMutability"": ""nonpayable"",
//      ""type"": ""function""
//    },
//    {
//      ""inputs"": [],
//      ""name"": ""getAllPolicies"",
//      ""outputs"": [
//        {
//          ""components"": [
//            {
//              ""internalType"": ""uint256"",
//              ""name"": ""userId"",
//              ""type"": ""uint256""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""fullName"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""phoneNumber"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""email"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""idCard"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""productName"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""packageName"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""benefitDetail"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""price"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""startDate"",
//              ""type"": ""string""
//            },
//            {
//              ""internalType"": ""string"",
//              ""name"": ""endDate"",
//              ""type"": ""string""
//            }
//          ],
//          ""internalType"": ""struct InsurancePolicy.Policy[]"",
//          ""name"": """",
//          ""type"": ""tuple[]""
//        }
//      ],
//      ""stateMutability"": ""view"",
//      ""type"": ""function""
//    }
//  ]


//";

        public HomeController(HealthInsuranceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            // Kiểm tra xem TempData có thông báo không
            if (TempData.ContainsKey("SuccessMessage"))
            {
                // Lấy thông báo từ TempData
                string successMessage = TempData["SuccessMessage"].ToString();

                // Truyền thông báo đến view thông qua ViewBag hoặc ViewModel
                ViewBag.SuccessMessage = successMessage;
            }
            return _context.Category != null ?
                          View(_context.Category.ToList()) :
                          Problem("Entity set 'HealthInsuranceContext.Category'  is null.");
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }


        public async  Task<IActionResult> Profile(int uId)
        {
            var userInfos = _context.UserInformation.Where(u => u.UserId == uId).FirstOrDefault();
            if (userInfos != null)
            {
                var web3 = new Web3("http://127.0.0.1:7545");
                
                var contractAddress = "0xb391e2610778F7b6424F850c7b8065989f19Cb0F";
                var url = "http://127.0.0.1:7545";
                //string abiJson = System.IO.File.ReadAllText("../HealthInsurance/wwwroot/abi/InsuranceContract.json");


                //var contract = web3.Eth.GetContract(abiJson, contractAddress);


                //var function = contract.GetFunction("getAllPolicies");

                ////var policies = await function.CallDeserializingToObjectAsync<List<Policy>>();

                //var po = await function.CallAsync<string>();

                var policyService = new PolicyService(contractAddress, url);

                List<Policy> policies = await policyService.GetAllPolicies();



                var userInfor = _context.UserInformation.Include(i => i.User).FirstOrDefault(n => n.InfId == userInfos.InfId);

                //var contract = _context.ContractInsurance.Include(a => a.InsuranceProducts).Where(c => c.UserId == uId).ToList();

                var packageIsr = _context.PackageInsurance.Include(p => p.InsuranceProducts).ToList();

                var profileModel = new ProfileModel
                {
                    userInformation = userInfor,
                    
                    packageInsurance = packageIsr,
                    insurancePolicies = policies


                };

                return View(profileModel);
                
            }

            return RedirectToAction("KYC", "Home");

        }




        public IActionResult KYC(int uId)
        {
            var userInfos = _context.UserInformation.Where(u => u.InfId == uId).FirstOrDefault();
            if (userInfos == null)
            {
                return View();
            }
            return RedirectToAction("Profile", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KYC([Bind("InfId,AvtImage,FullName,Birthday,Sex,CardId,LicenseDate,FrontImage,BackImage,PhoneNumber,Address,UserId")] UserInformation userInformation, 
             IFormFile frontImage, IFormFile backImage, IFormFile avtImage, string sex)
        {
            if (frontImage != null && frontImage.Length > 0 
                && backImage != null && backImage.Length > 0 
                && avtImage != null && avtImage.Length > 0)
            {
                var frontFileName = Path.GetFileName(frontImage.FileName);
                var frontFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/images", frontFileName);
                userInformation.FrontImage = frontFileName;

                var backFileName = Path.GetFileName(backImage.FileName);
                var backFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/images", backFileName);
                userInformation.BackImage = backFileName;

                var avtFileName = Path.GetFileName(avtImage.FileName);
                var avtFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/images", avtFileName);
                userInformation.AvtImage = avtFileName;

                userInformation.Sex = sex;

                var userId = int.Parse(User.FindFirst("UserId")?.Value);
                userInformation.UserId = userId;

                _context.Add(userInformation);
                _context.SaveChanges();

                using (var fileStream1 = new FileStream(frontFilePath, FileMode.Create))
                {
                    await frontImage.CopyToAsync(fileStream1);
                }
                using (var fileStream2 = new FileStream(backFilePath, FileMode.Create))
                {
                    await backImage.CopyToAsync(fileStream2);
                }
                using (var fileStream3 = new FileStream(avtFilePath, FileMode.Create))
                {
                    await avtImage.CopyToAsync(fileStream3);
                }

                return RedirectToAction("Index", "Home");

            }else if (ModelState.IsValid)
            {
                var frontFileName = Path.GetFileName(frontImage.FileName);
                var frontFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/images", frontFileName);
                userInformation.FrontImage = frontFileName;

                var backFileName = Path.GetFileName(backImage.FileName);
                var backFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/images", backFileName);
                userInformation.BackImage = backFileName;

                var avtFileName = Path.GetFileName(avtImage.FileName);
                var avtFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProjectLayout/images", avtFileName);
                userInformation.AvtImage = avtFileName;

                userInformation.Sex = sex;

                var userId = int.Parse(User.FindFirst("UserId")?.Value);
                userInformation.UserId = userId;

                _context.Add(userInformation);
                _context.SaveChanges();

                using (var fileStream1 = new FileStream(frontFilePath, FileMode.Create))
                {
                    await frontImage.CopyToAsync(fileStream1);
                }
                using (var fileStream2 = new FileStream(backFilePath, FileMode.Create))
                {
                    await backImage.CopyToAsync(fileStream2);
                }
                using (var fileStream3 = new FileStream(avtFilePath, FileMode.Create))
                {
                    await avtImage.CopyToAsync(fileStream3);
                }

                return RedirectToAction("Index", "Home");
            }


              
            
            return View(userInformation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConsult([Bind("ConsultId,ClientName,PhoneNumber,InsuranceName,Email,Message")] ConsultMessage consultMessage, string ClientName, string PhoneNumber, string InsuranceType, string Email, string Message)
        {
            if (ModelState.IsValid)
            {
                consultMessage.ClientName = ClientName;
                consultMessage.PhoneNumber = PhoneNumber;
                consultMessage.InsuranceName = InsuranceType;
                consultMessage.Email = Email;
                consultMessage.Message = Message;

                _context.Add(consultMessage);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Gửi yêu cầu thành công.";
                return RedirectToAction("Index", "Home");
            }
            TempData["SuccessMessage"] = "Gửi yêu cầu không thành công. Vui lòng nhập đầy đủ nội dung!";
            return RedirectToAction("Index", "Home");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}