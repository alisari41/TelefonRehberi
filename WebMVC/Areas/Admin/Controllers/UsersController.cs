using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            var result = _authService.GetList();
            if (result.Success)
            {//Eğer doğru çalıştıysa datayı getir
                return View(result.Data);
            }
            return View(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExits = _authService.UserExists(userForRegisterDto.Email); //Öncelikle kullanıcı var mı ona sonucuna işlemler zaten busniessta yapılıyor bakmak gerekiyor
            if (!userExits.Success)//Kullanıcı varsa sonucu
            {//API tarafın ne sonuç dönecek ona bakıyoruz Diğer işlemler zaten İş katmanında yapılıyor.

                ViewBag.Error = userExits.Message;
                return View();
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);//Kullanıcıyı kayıt ettik
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {

                HttpContext.Session.SetString("Token", result.Data.Token);
                return Redirect("/admin");//kendini admin sayfasına gönder
            }

            ViewBag.Error = result.Message;
            return View();
        }
    }
}
