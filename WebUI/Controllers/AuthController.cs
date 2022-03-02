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
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                ViewBag.Error = "Giriş Yapılamadı. " + userToLogin.Message;
                return View("Index");
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (!result.Success)
            {
                ViewBag.Error = "Giriş Yapılamadı. " + result.Message;
                return View("Index");
            }

            HttpContext.Session.SetString("Authorization", "Bearer " + result.Data.Token);
            return RedirectToAction("Index", "Exam");
        }

        public ActionResult Register()
        {
            return View(new UserForLoginDto());
        }

        [HttpPost]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                ViewBag.Error = "Kulllanıcı Kaydı Yapılamadı. " + userExists.Message;
                return View("Register");
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (!registerResult.Success)
            {
                ViewBag.Error = "Kulllanıcı Kaydı Yapılamadı. " + registerResult.Message;
                return View("Register");
            }


            ViewBag.Success = true;
            ViewBag.Error = "Üye Kaydınız Yapıldı. Lütfen Giriş Yapınız.";
            return View("Login");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
