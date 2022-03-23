using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private IPhoneAddressService _phoneAddressService;

        public HomeController(IPhoneAddressService phoneAddressService)
        {
            _phoneAddressService = phoneAddressService;
        }

        public IActionResult Index()
        {
            var result =  _phoneAddressService.GetList();
            if (result.Data.Count == 0)
            {
                return RedirectToAction("Warring");
            }
            return View(result.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
