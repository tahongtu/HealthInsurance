using HealthInsurance.Data;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthInsurance.Controllers
{
    public class AccountController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public AccountController(HealthInsuranceContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }
  


        public IActionResult Signup()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("UserId,UserName,UserEmail,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }






        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User _userFromPage)
        {
            var _user = _context.User.Where(m => m.UserName == _userFromPage.UserName && m.Password == _userFromPage.Password).FirstOrDefault();

            if (_user == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.UserName),
                    new Claim("Email", _user.UserEmail),
                    new Claim(ClaimTypes.Role, _user.Role),
                    new Claim("UserId", _user.UserId.ToString()),
                };

                


                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    
                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                if (_user.Role == "Administrator")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

               
            }

            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
