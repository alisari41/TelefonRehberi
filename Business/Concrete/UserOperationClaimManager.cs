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
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IDataResult<UserOperationClaim> GetById(int userOperationClaimId)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(p => p.Id == userOperationClaimId));
        }

        [CacheAspect(100)]//Duration Cache'te ne kadar dakika kalıcak değeri veriyorum vermezsem 60 sabit ayarladım.
        [PerformanceAspect(5)]//Eğer verdiğim saniyeyi(5) geçerse output'a yazıcak
        public IDataResult<List<UserOperationClaim>> GetList()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetList().ToList());
        }
        //Doğrulama işlemini FluentValidation olarak yaptım
        [ValidationAspect(typeof(UserOperationClaimValidator), Priority = 1)]//Priority sıralama
        [CacheRemoveAspect("IUserOperationClaimService.Get")]// Yeni Ürün Eklendiğin Ön Belleği temizle. İçerisinde IProductService.Get olanları Yani Başı 
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);

            //"Rol başarıyla eklendi."  parantez içinde bunu kullanmak Magic Stringlere giriyor yani bu mesajı bir çok yerde kullandığımı varsayarsak
            //Bunu değiştirmek istediğimizde çok zorlanacağız. O yüzden Magic Stringlerden kurtulmak için bir sınıf oluşturdum mesajları oradan çekiyorum
            return new SuccessResult(Messages.UserOperationClaimAdded);

        }
        [CacheRemoveAspect("IOperationClaimService.Get")]// Yeni Ürün Eklendiğin Ön Belleği temizle. İçerisinde IProductService.Get olanları Yani Başı 
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }
        //Doğrulama işlemini FluentValidation olarak yaptım
        [ValidationAspect(typeof(UserOperationClaimValidator), Priority = 1)]//Priority sıralama
        [CacheRemoveAspect("IUserOperationClaimService.Get")]// Yeni Ürün Eklendiğin Ön Belleği temizle. İçerisinde IProductService.Get olanları Yani 
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }
    }
}
