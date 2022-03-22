﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AddressController : Controller
    {
        private IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public IActionResult Index()
        {
            var result = _addressService.GetList();
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
        public IActionResult Add(Address address)
        {

            try
            {
                var adressValidator = new AddressValidator();
                var validationResult = adressValidator.Validate(address);

                if (validationResult.IsValid)
                {//Eğer doğru çalıştıysa datayı getir
                    var result = _addressService.Add(address);
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
            var result = _addressService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Edit(Address address)
        {
            try
            {
                var adressValidator = new AddressValidator();
                var validationResult = adressValidator.Validate(address);

                if (validationResult.IsValid)
                {//Eğer doğru çalıştıysa datayı getir
                    var result = _addressService.Update(address);
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
            var result = _addressService.GetById(id);
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Delete(Address address)
        {
            try
            {
                var result = _addressService.Delete(address);
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
