using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;

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


        public IActionResult GetClaims(int id)
        {
            User user = new User();
            user.Id = id;
            var result = _userService.GetClaimsList(user);
            if (result.Data.Count == 0)
            {
                return (RedirectToAction("Warring"));
            }
            return View(result.Data);
        }

        public IActionResult Warring()
        {
            ViewBag.Message = "Kullanıcının Rolü / Görevi bulunmamaktadır!!";
            return View();
        }
    }
}
