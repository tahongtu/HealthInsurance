using HealthInsurance.Data;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HealthInsurance.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly HealthInsuranceContext _context;

        public HomeController(HealthInsuranceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
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


        public IActionResult Profile(int uId)
        {
            var userInfos = _context.UserInformation.Where(u => u.UserId == uId).FirstOrDefault();
            if (userInfos != null)
            {
                var userInformation = _context.UserInformation.Include(i => i.User).FirstOrDefault(n => n.InfId == userInfos.InfId);

                return View(userInformation);
                
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






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}