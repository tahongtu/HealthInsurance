using HealthInsurance.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HealthInsurance.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace HealthInsurance.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        private readonly HealthInsuranceContext _context;

        public AdminController(HealthInsuranceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userInfos = _context.UserInformation.Include(i => i.User).ToList();
            
            var package = _context.PackageInsurance.Include(p => p.InsuranceProducts).ToList();
            var contract = _context.ContractInsurance.ToList();

            var dashboardModel = new DashBoardModel
            {
                userInformation = userInfos,
                packageInsurances = package,
                contractInsurances = contract
            };


            return View(dashboardModel);
        }




    }
}
