using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimDal _operationClaim;

        public OperationClaimManager(IOperationClaimDal operationClaim)
        {
            _operationClaim = operationClaim;
        }

        public IDataResult<OperationClaim> GetById(int operationClaimId)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaim.Get(p => p.Id == operationClaimId));
        }

        [CacheAspect(100)]//Duration Cache'te ne kadar dakika kalıcak değeri veriyorum vermezsem 60 sabit ayarladım.
        [PerformanceAspect(5)]//Eğer verdiğim saniyeyi(5) geçerse output'a yazıcak
        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaim.GetList().ToList());
        }

        //Doğrulama işlemini FluentValidation olarak yaptım
        [ValidationAspect(typeof(OperationClaimValidator), Priority = 1)]//Priority sıralama
        [CacheRemoveAspect("IOperationClaimService.Get")]// Yeni Ürün Eklendiğin Ön Belleği temizle. İçerisinde IProductService.Get olanları Yani Başı Get İle 
        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(operationClaim.Name));
            if (result != null)
            {
                return result;
            }


            _operationClaim.Add(operationClaim);


            //"Rol başarıyla eklendi."  parantez içinde bunu kullanmak Magic Stringlere giriyor yani bu mesajı bir çok yerde kullandığımı varsayarsak
            //Bunu değiştirmek istediğimizde çok zorlanacağız. O yüzden Magic Stringlerden kurtulmak için bir sınıf oluşturdum mesajları oradan çekiyorum
            return new SuccessResult(Messages.OperationClaimAdded);
        }
        private IResult CheckIfProductNameExists(string claimName)
        {//Metodlar çoğunlukla IResult kullanılıyor dikkat et

            var result = _operationClaim.GetList(p => p.Name == claimName).Any();
            if (result)
            {//Eğer girilen ürün adı sistemde varsa
                return new ErrorResult(Messages.OperationClaimNameAlreadyExists);
            }

            return new SuccessResult();//Boş bir successResult dönerse sorun yok Diğer metodda okumak için 
        }
        
        [CacheRemoveAspect("IOperationClaimService.Get")]// Yeni Ürün Eklendiğin Ön Belleği temizle. İçerisinde IProductService.Get olanları Yani Başı 
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaim.Delete(operationClaim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        //Doğrulama işlemini FluentValidation olarak yaptım
        [ValidationAspect(typeof(OperationClaimValidator), Priority = 1)]//Priority sıralama
        [CacheRemoveAspect("IOperationClaimService.Get")]// Yeni Ürün Eklendiğin Ön Belleği temizle. İçerisinde IProductService.Get olanları Yani Başı 
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaim.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }
    }
}
