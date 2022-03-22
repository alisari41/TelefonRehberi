using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using WebAPI.CalismaDurumu;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelephoneDirectoriesController : ControllerBase
    {
        private ITelephoneDirectoryService _telephoneDirectoryService;
        private CalismaDurumlari _calismaDurumlari = new CalismaDurumlari();//Bütün durumları tek bir yerde kullanmak için Her metod içerisinde yapmak yerine 

        public TelephoneDirectoriesController(ITelephoneDirectoryService telephoneDirectoryService)
        {
            _telephoneDirectoryService = telephoneDirectoryService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _telephoneDirectoryService.GetList();
            return _calismaDurumlari.CalismaDurumuList(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int telephoneDirectoryId)
        {
            var result = _telephoneDirectoryService.GetById(telephoneDirectoryId);
            return _calismaDurumlari.CalismaDurumuByid(result);
        }

        [HttpPost("add")]
        public IActionResult Add(TelephoneDirectories telephoneDirectories)
        {
            var result = _telephoneDirectoryService.Add(telephoneDirectories);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(TelephoneDirectories telephoneDirectories)
        {
            var result = _telephoneDirectoryService.Delete(telephoneDirectories);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(TelephoneDirectories telephoneDirectories)
        {
            var result = _telephoneDirectoryService.Update(telephoneDirectories);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }
    }
}
