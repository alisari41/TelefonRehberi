using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Concrete;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserOperationClaimsController : Controller
    {
        private IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        public IActionResult Index()
        {
            var result = _userOperationClaimService.GetList();
            if (result.Success)
            {
                return View(result.Data);
            }
            return View(result.Message);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserOperationClaim userOperationClaim)
        {
            try
            {
                var userOperationClaimValidator = new UserOperationClaimValidator();
                var validationResult = userOperationClaimValidator.Validate(userOperationClaim);

                if (validationResult.IsValid)
                {//Eğer doğru çalıştıysa datayı getir
                    var result = _userOperationClaimService.Add(userOperationClaim);
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
        public IActionResult Edit(int id)
        {
            var result = _userOperationClaimService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Edit(UserOperationClaim userOperationClaim)
        {
            try
            {
                var userOperationClaimValidator = new UserOperationClaimValidator();
                var validationResult = userOperationClaimValidator.Validate(userOperationClaim);

                if (validationResult.IsValid)
                {//Eğer doğru çalıştıysa datayı getir
                    var result = _userOperationClaimService.Update(userOperationClaim);
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
            var result = _userOperationClaimService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Delete(UserOperationClaim userOperationClaim)
        {
            try
            {
                var result = _userOperationClaimService.Delete(userOperationClaim);
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


            return View();
        }
    }
}
