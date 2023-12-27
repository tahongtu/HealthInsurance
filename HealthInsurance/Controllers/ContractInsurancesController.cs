using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Data;
using HealthInsurance.Models;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Newtonsoft.Json;
using Nethereum.Contracts;

namespace HealthInsurance.Controllers
{
    public class ContractInsurancesController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public ContractInsurancesController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: ContractInsurances
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.ContractInsurance.Include(c => c.InsuranceProducts).Include(c => c.User);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: ContractInsurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContractInsurance == null)
            {
                return NotFound();
            }

            var contractInsurance = await _context.ContractInsurance
                .Include(c => c.InsuranceProducts)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contractInsurance == null)
            {
                return NotFound();
            }

            return View(contractInsurance);
        }

        // GET: ContractInsurances/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password");
            return View();
        }

        // POST: ContractInsurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,DateStart,DateEnd,ProductId,UserId")] ContractInsurance contractInsurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractInsurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", contractInsurance.ProductId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", contractInsurance.UserId);
            return View(contractInsurance);
        }

        // GET: ContractInsurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContractInsurance == null)
            {
                return NotFound();
            }

            var contractInsurance = await _context.ContractInsurance.FindAsync(id);
            if (contractInsurance == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", contractInsurance.ProductId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", contractInsurance.UserId);
            return View(contractInsurance);
        }

        // POST: ContractInsurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,DateStart,DateEnd,ProductId,UserId")] ContractInsurance contractInsurance)
        {
            if (id != contractInsurance.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractInsurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractInsuranceExists(contractInsurance.ContractId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.InsuranceProducts, "ProductId", "Description", contractInsurance.ProductId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", contractInsurance.UserId);
            return View(contractInsurance);
        }

        // GET: ContractInsurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContractInsurance == null)
            {
                return NotFound();
            }

            var contractInsurance = await _context.ContractInsurance
                .Include(c => c.InsuranceProducts)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contractInsurance == null)
            {
                return NotFound();
            }

            return View(contractInsurance);
        }

        // POST: ContractInsurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContractInsurance == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.ContractInsurance'  is null.");
            }
            var contractInsurance = await _context.ContractInsurance.FindAsync(id);
            if (contractInsurance != null)
            {
                _context.ContractInsurance.Remove(contractInsurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractInsuranceExists(int id)
        {
          return (_context.ContractInsurance?.Any(e => e.ContractId == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> Contract(int prdId, int packageId)
        {
            

            var insuranceProducts = await _context.InsuranceProducts
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ProductId == prdId);
            
            if (insuranceProducts == null)
            {
                return NotFound();
            }
            var benefitDetail = _context.BenefitDetail.Where(b => b.ProductId == prdId).ToList();
            var diseaseList = _context.DiseaseList.Where(d => d.ProductId == prdId).ToList();
            var outstandingBenefit = _context.OutstandingBenefit.Where(o => o.ProductId == prdId).ToList();
            var participationInfos = _context.ParticipationInformation.Where(p => p.ProductId == prdId).ToList();
            var packageInsurance = await _context.PackageInsurance.Include(n => n.InsuranceProducts).FirstOrDefaultAsync(n => n.PackageId == packageId);
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId);
            var userInfos = await _context.UserInformation.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId);

            var viewModel = new ContractInformationModel
            {
                insuranceProducts = insuranceProducts,
                benefitDetails = benefitDetail,
                diseaseLists = diseaseList,
                outstandingBenefits = outstandingBenefit,
                participationInformation = participationInfos,
                packageInsurances = packageInsurance,
                user = user,
                userInformation = userInfos

            };
            return View(viewModel);
        }

        public async Task<IActionResult> AddBlock([Bind("ContractId,DateStart,DateEnd,ProductId,UserId")] ContractInsurance contractInsurance,int prdId, int packageId)
        {

            //Add to database
            var insuranceProducts = await _context.InsuranceProducts
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ProductId == prdId);

            if (insuranceProducts == null)
            {
                return NotFound();
            }
            var benefitDetail = _context.BenefitDetail.Where(b => b.ProductId == prdId).ToList();
            var diseaseList = _context.DiseaseList.Where(d => d.ProductId == prdId).ToList();
            var outstandingBenefit = _context.OutstandingBenefit.Where(o => o.ProductId == prdId).ToList();
            var participationInfos = _context.ParticipationInformation.Where(p => p.ProductId == prdId).ToList();
            var packageInsurance = await _context.PackageInsurance.Include(n => n.InsuranceProducts).FirstOrDefaultAsync(n => n.PackageId == packageId);
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId);
            var userInfos = await _context.UserInformation.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId);


            contractInsurance.DateStart = DateTime.Now.Date;
            contractInsurance.DateEnd = DateTime.Now.AddYears(1).Date;
            contractInsurance.ProductId = insuranceProducts.ProductId;
            contractInsurance.UserId = userId;
            _context.Add(contractInsurance);
            await _context.SaveChangesAsync(); 



            //Add to blockchain

            var web3 = new Web3("http://127.0.0.1:7545"); // Địa chỉ RPC endpoint của Ganache
            

            var contractAddress = "0x0c10424eCeE1622b24d0BAfD7636EAf3082BEb06"; // Địa chỉ của smart contract đã triển khai trên Ganache


            string abiJson = @"
[
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""internalType"": ""uint256"",
          ""name"": ""policyId"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""PolicyCreated"",
      ""type"": ""event""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""policies"",
      ""outputs"": [
        {
          ""internalType"": ""string"",
          ""name"": ""customerName"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""customerAddress"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""phoneNumber"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""birthday"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""email"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""sex"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""idCard"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""policyTerms"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""premiumAmount"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""policyStartDate"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""policyEndDate"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""bool"",
          ""name"": ""isClaimed"",
          ""type"": ""bool""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [],
      ""name"": ""policyCount"",
      ""outputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": """",
          ""type"": ""uint256""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""string"",
          ""name"": ""_customerName"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_customerAddress"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_phoneNumber"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_birthday"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_email"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_sex"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_idCard"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_policyTerms"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""uint256"",
          ""name"": ""_premiumAmount"",
          ""type"": ""uint256""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_policyStartDate"",
          ""type"": ""string""
        },
        {
          ""internalType"": ""string"",
          ""name"": ""_policyEndDate"",
          ""type"": ""string""
        }
      ],
      ""name"": ""createPolicy"",
      ""outputs"": [],
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""internalType"": ""uint256"",
          ""name"": ""_policyId"",
          ""type"": ""uint256""
        }
      ],
      ""name"": ""getPolicy"",
      ""outputs"": [
        {
          ""components"": [
            {
              ""internalType"": ""string"",
              ""name"": ""customerName"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""customerAddress"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""phoneNumber"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""birthday"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""email"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""sex"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""idCard"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""policyTerms"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""uint256"",
              ""name"": ""premiumAmount"",
              ""type"": ""uint256""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""policyStartDate"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""string"",
              ""name"": ""policyEndDate"",
              ""type"": ""string""
            },
            {
              ""internalType"": ""bool"",
              ""name"": ""isClaimed"",
              ""type"": ""bool""
            }
          ],
          ""internalType"": ""struct HealthInsuranceContract.Policy"",
          ""name"": """",
          ""type"": ""tuple""
        }
      ],
      ""stateMutability"": ""view"",
      ""type"": ""function""
    }
  ]
";
            //ContractABI contractABI = ContractABI.FromJson(abiJson);  // ABI của smart contract
            var contract = web3.Eth.GetContract(abiJson, contractAddress);
            

            var createFunction = contract.GetFunction("createPolicy");

            var customerName = userInfos.FullName.ToString();
            var customerAddress = userInfos.Address.ToString();
            var phoneNumber = userInfos.PhoneNumber.ToString();
            var birthday = userInfos.Birthday.ToString();
            var email = user.UserEmail.ToString();
            var sex = userInfos.Sex.ToString();
            var idCard = userInfos.CardId.ToString();
            var policyTerms = insuranceProducts.ProductName.ToString();
            var premiumAmount = packageInsurance.Price;
            var policyStartDate = DateTime.Now.Date.ToString();
            var policyEndDate = DateTime.Now.AddYears(1).Date.ToString();

            

            
             



            // Gửi giao dịch và chờ nhận hồi đáp
            var transactionReceipt = await createFunction.SendTransactionAndWaitForReceiptAsync(
                    contractAddress,
                    
                    new HexBigInteger(900000),
                    new HexBigInteger(0),
                    null,
                    customerName,
                    customerAddress,
                    phoneNumber,
                    birthday,
                    email,
                    sex,
                    idCard,
                    policyTerms,
                    premiumAmount,
                    policyStartDate,
                    policyEndDate
                );


            return RedirectToAction("Index", "Home");
        }

        



        }
}
