using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserClaimListsController : Controller
    {
        private IUserService _userService;

        public UserClaimListsController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var result = _userService.GetList();
            if (result.Success)
            {//Eğer doğru çalıştıysa datayı getir
                return View(result.Data);
            }
            return View(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }

        

        public IActionResult GetClaims(int id,string name,string lastName,string email)
        {
            User user = new User();
            user.Id = id;
            ViewBag.Ad = name;
            ViewBag.Soyad = lastName;
            ViewBag.Email = email;

            var result = _userService.GetClaimsList(user);
            if (result.Data.Count == 0)
            {
                return (RedirectToAction("Warring"));
            }
            return View(result.Data);
        }
        public IActionResult Delete(int id)
        {
            var result = _userService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            try
            {
                var result = _userService.Delete(user);
                if (result.Success)
                {//Eğer doğru çalıştıysa datayı getir
                    ViewBag.Success = result.Success;
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = result.Message;
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }


            return View();//Eğer Hatalı ise Mesaj dönder.
        }
        public IActionResult Warring()
        {
            ViewBag.Message = "Kullanıcının Rolü / Görevi bulunmamaktadır!!";
            return View();
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
