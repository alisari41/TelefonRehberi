using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Concrete;
using FormHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OperationClaimsController : Controller
    {
        private IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        public IActionResult Index()
        {
            var result = _operationClaimService.GetList();
            if (result.Success)
            {//Eğer doğru çalıştıysa datayı getir
                return View(result.Data);
            }
            return View(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(OperationClaim operationClaim)
        {

            try
            {
                var operationClaimValidator = new OperationClaimValidator();
                var validationResult = operationClaimValidator.Validate(operationClaim);

                if (validationResult.IsValid)
                {//Eğer doğru çalıştıysa datayı getir
                    var result = _operationClaimService.Add(operationClaim);
                    if (result.Success)
                    {
                        ViewBag.Success = result.Success;
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.Message = result.Message;
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                //foreach (var item in e.)
                //{
                //    ModelState.AddModelError(item);
                //}
            }


            return View();
        }

        public IActionResult Edit(int id)
        {
            var result = _operationClaimService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Edit(OperationClaim operationClaim)
        {
            try
            {
                var operationClaimValidator = new OperationClaimValidator();
                var validationResult = operationClaimValidator.Validate(operationClaim);

                if (validationResult.IsValid)
                {//Eğer doğru çalıştıysa datayı getir
                    var result = _operationClaimService.Update(operationClaim);
                    if (result.Success)
                    {
                        ViewBag.Success = result.Success;
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.Message = result.Message;
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
          

            return View();
        }
        public IActionResult Delete(int id)
        {
            var result = _operationClaimService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Delete(OperationClaim operationClaim)
        {
            try
            {
                var result = _operationClaimService.Delete(operationClaim);
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

        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
