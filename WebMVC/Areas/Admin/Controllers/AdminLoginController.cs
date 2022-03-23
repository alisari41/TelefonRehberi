using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {

        private IAuthService _authService;

        public AdminLoginController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)//işlem başarılı değilse
            {
                ViewBag.Error = userToLogin.Message;
                return (RedirectToAction("Error"));
            }
            //Eğer buraya geliyorsa zaten doğru dönmüş demek return dönüş yapıp çıktığı için 
            IActionResult response = Unauthorized();
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                //return Ok(result.Data);
                HttpContext.Session.SetString("Token", result.Data.Token);
                return Redirect("/admin");//kendini admin sayfasına gönder
            }

            ViewBag.Error = result.Message;
            return (RedirectToAction("Error"));
        }

        public IActionResult Error()
        {
            ViewBag.Message = "An error occured...";
            return View();
        }


       


    }
}


//private readonly IConfiguration _config;
//private readonly IUserRepository _userRepository;
//private readonly ITokenService _tokenService;
//private string generatedToken = null;

//public HomeController (IConfiguration config, ITokenService tokenService, IUserRepository userRepository)
//{
//    _config = config;
//    _tokenService = tokenService;
//    _userRepository = userRepository;
//}

//public IActionResult Index()
//{
//    return View();
//}

//[AllowAnonymous]
//[Route("login")]
//[HttpPost]
//public IActionResult Login(UserModel userModel)
//{
//    if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
//    {
//        return (RedirectToAction("Error"));
//    }
//    IActionResult response = Unauthorized();
//    var validUser = GetUser(userModel);

//    if (validUser != null)
//    {
//        generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
//        if (generatedToken != null)
//        {
//            HttpContext.Session.SetString("Token", generatedToken);
//            return RedirectToAction("MainWindow");
//        }
//        else
//        {
//            return (RedirectToAction("Error"));
//        }
//    }
//    else
//    {
//        return (RedirectToAction("Error"));
//    }
//}

//private UserDTO GetUser(UserModel userModel)
//{
//    // Write your code here to authenticate the user     
//    return _userRepository.GetUser(userModel);
//}

//[Authorize]
//[Route("mainwindow")]
//[HttpGet]
//public IActionResult MainWindow()
//{
//    string token = HttpContext.Session.GetString("Token");
//    if (token == null)
//    {
//        return (RedirectToAction("Index"));
//    }
//    if (!_tokenService.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
//    {
//        return (RedirectToAction("Index"));
//    }
//    ViewBag.Message = BuildMessage(token, 50);
//    return View();
//}

//public IActionResult Error()
//{
//    ViewBag.Message = "An error occured...";
//    return View();
//}

//private string BuildMessage(string stringToSplit, int chunkSize)
//{
//    var data = Enumerable.Range(0, stringToSplit.Length / chunkSize).Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));
//    string result = "The generated token is:";
//    foreach (string str in data)
//    {
//        result += Environment.NewLine + str;
//    }
//    return result;
//}
//    }    
