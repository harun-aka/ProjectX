using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    
    public class AuthController : Controller
    {

        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public ActionResult Login()
        {
            return View(new UserForLoginDto());
        }

        [HttpPost]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                ViewBag.Error = "Log in failed. " + userToLogin.Message;
                return View("Login");
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (!result.Success)
            {
                ViewBag.Error = "Log in failed. " + result.Message;
                return View("Login");
            }

            HttpContext.Session.SetString("Authentication", "Bearer " + result.Data.Token);
            return RedirectToAction("Index", "Exam");
        }

        public ActionResult Register()
        {
            return View(new UserForRegisterDto());
        }

        [HttpPost]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            var userResult = _authService.UserExists(userForRegisterDto.Email);
            if (!userResult.Success)
            {
                ViewBag.Error = "Sign In Failed. " + userResult.Message;
                return View("Register");
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (!registerResult.Success)
            {
                ViewBag.Error = "Sign In Failed. " + registerResult.Message;
                return View("Register");
            }


            ViewBag.Success = true;
            ViewBag.Error = "User Created. Please Log In.";
            return View("Login");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
