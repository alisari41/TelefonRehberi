﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DefaultController : AdminControllerBase
    {


        //[Authorize]
        public IActionResult Index()
        {
            ViewBag.Token = HttpContext.Session.GetString("Token");
            var token = HttpContext.Session.GetString("Token");
            if (token==null)
            {
                return Redirect("Admin/AdminLogin/Index");
            }

            var apiToken = HttpContext.Session.GetString("Token");


            ViewBag.Message = BuildMessage(token, 50);
            return View();
        }
        private string BuildMessage(string stringToSplit, int chunkSize)
        {
            var data = Enumerable.Range(0, stringToSplit.Length / chunkSize).Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));
            string result = "The generated token is:";
            foreach (string str in data)
            {
                result += Environment.NewLine + str;
            }
            return result;
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
