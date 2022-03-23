using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebMVC.Areas.Admin
{
    public class AdminControllerBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var IsLogin = false;
            if (context.HttpContext.Session.GetString("Token") == null)
            {
                //admin girişi yapılmamış
                context.HttpContext.Response.Redirect("Admin/AdminLogin/Index");
                //admin girişe sayfayı yönlendir.
                //redirect("....")bunu kullanamadık redirectte ezme işlemi yaptık override sayfa yönlendirir.
            }
            else
            {
                //sorun yok admin ieçerde
                //sayfayı çalıştır
                base.OnActionExecuting(context);

            }
        }
    }
}
