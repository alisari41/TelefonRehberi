using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using WebAPI.CalismaDurumu;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private IOperationClaimService _operationClaimService;
        private CalismaDurumlari _calismaDurumlari = new CalismaDurumlari();//Bütün durumları tek bir yerde kullanmak için Her metod içerisinde yapmak yerine tek 

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _operationClaimService.GetList();
            //if (result.Success)
            //{//Eğer doğru çalıştıysa datayı getir
            //    return Ok(result.Data);
            //}

            //return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
            //Yukardaki kod bloğunu sürekli kullandığım için metod içine taşıdım.
            return _calismaDurumlari.CalismaDurumuList(result);
        }

        [HttpPost("add")]
        public IActionResult Add(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Add(operationClaim);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("delete")] //HttpPost kullanılır
        public IActionResult Delete(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Delete(operationClaim);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }

        [HttpPost("update")]
        public IActionResult Update(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Update(operationClaim);
            return _calismaDurumlari.CalismaDurumuCRUD(result);
        }
    }
}
