using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Concrete;
using Entities.Concrete;
using WebMVC.Models;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TelephoneDirectoriesController : Controller
    {

        private ITelephoneDirectoryService _telephoneDirectoryService;

        public TelephoneDirectoriesController(ITelephoneDirectoryService telephoneDirectoryService)
        {
            _telephoneDirectoryService = telephoneDirectoryService;
        }

        public IActionResult Index()
        {
            try
            {
                var result = _telephoneDirectoryService.GetList();
                if (result.Success)
                {//Eğer doğru çalıştıysa datayı getir
                    return View(result.Data);
                }
                return View(result.Message);//Eğer Hatalı ise Mesaj dönder.
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Warring", new { message = e.Message });
            }
        }
        public IActionResult Warring(string message)
        {
            ViewBag.Message = message;
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(FotografEkle fotografEkle)
        {
            try
            {
                TelephoneDirectories telephoneDirectories = new TelephoneDirectories();

                if (fotografEkle.PhotoUrl != null)
                {
                    var extension = Path.GetExtension(fotografEkle.PhotoUrl.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures/", newImageName);//Seçilen dosya yolu
                    var stream = new FileStream(location, FileMode.Create);
                    fotografEkle.PhotoUrl.CopyTo(stream);
                    telephoneDirectories.PhotoUrl = newImageName;
                }

                telephoneDirectories.AddressId = fotografEkle.AddressId;
                telephoneDirectories.FirstName = fotografEkle.FirstName;
                telephoneDirectories.LastName = fotografEkle.LastName;
                telephoneDirectories.Title = fotografEkle.Title;
                telephoneDirectories.Email = fotografEkle.Email;
                telephoneDirectories.PhoneNumber = fotografEkle.PhoneNumber;
                telephoneDirectories.Fax = fotografEkle.Fax;
                telephoneDirectories.InternalNumber = fotografEkle.InternalNumber;


                
                var telephoneDirectoryValidator = new TelephoneDirectoryValidator();
                var validationResult = telephoneDirectoryValidator.Validate(telephoneDirectories);

                if (validationResult.IsValid)
                {//Eğer doğru çalıştıysa datayı getir
                    var result = _telephoneDirectoryService.Add(telephoneDirectories);
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
            var result = _telephoneDirectoryService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Edit(TelephoneDirectories telephoneDirectories)
        {
            try
            {
                var telephoneDirectoryValidator = new TelephoneDirectoryValidator();
                var validationResult = telephoneDirectoryValidator.Validate(telephoneDirectories);

                if (validationResult.IsValid)
                {
                    var result = _telephoneDirectoryService.Update(telephoneDirectories);
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
            var result = _telephoneDirectoryService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Delete(TelephoneDirectories telephoneDirectories)
        {
            try
            {
                var result = _telephoneDirectoryService.Delete(telephoneDirectories);
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

    }
}
