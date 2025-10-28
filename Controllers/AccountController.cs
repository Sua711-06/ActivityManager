using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PhotoAlbum.Controllers {
    public class AccountController: Controller {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration) {
            _configuration = configuration;
        }

        // GET: /Account/Login
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string returnUrl) {
            if(username == _configuration["username"] && password == _configuration["password"]) {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "admin"),
                    new Claim(ClaimTypes.Name, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                if(!string.IsNullOrEmpty(returnUrl)) {
                    return Redirect(returnUrl);
                } else {
                    return RedirectToAction("Index", "Home");
                }

            }

            ViewBag.ErrorMessage = "Invalid username or password.";

            return View();
        }

        public IActionResult Logout() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirmed() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }
    }
}