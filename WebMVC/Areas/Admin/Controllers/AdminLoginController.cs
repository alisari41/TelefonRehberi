using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Entities.Dtos;
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
            //return View();
            //try
            //{


            //   //Eğer doğru çalıştıysa datayı getir
            //        var result = _operationClaimService.Add(operationClaim);
            //        if (result.Success)
            //        {
            //            ViewBag.Success = result.Success;
            //            return RedirectToAction(nameof(Index));
            //        }
            //        ViewBag.Message = result.Message;
            //    }


            //}
            //catch (Exception e)
            //{
            //    ViewBag.Error = e.Message;
            //    //foreach (var item in e.)
            //    //{
            //    //    ModelState.AddModelError(item);
            //    //}
            //}


            return View();
        }
        [HttpPost]
        public IActionResult Index(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)//işlem başarılı değilse
            {
                ViewBag.UserNotFound = userToLogin.Message;
                return View();
            }
            //Eğer buraya geliyorsa zaten doğru dönmüş demek return dönüş yapıp çıktığı için 
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                //return Ok(result.Data);
                HttpContext.Session.SetString("Token", result.Data.Token);
                return Redirect("/admin");//kendini admin sayfasına gönder
            }

            ViewBag.Error = result.Message;
            return View();
        }


    }
}
