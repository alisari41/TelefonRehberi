using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DefaultController : AdminControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
