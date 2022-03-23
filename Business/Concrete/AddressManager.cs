using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AddressManager : IAddressService
    {
        private IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }


        [CacheAspect(100)]
        public IDataResult<Address> GetById(int addressId)
        {
            return new SuccessDataResult<Address>(_addressDal.Get(addressId));
        }

        [SecuredOperation("Admin")]
        [CacheAspect(100)]
        [PerformanceAspect(5)]
        public IDataResult<List<Address>> GetList()
        {
            return new SuccessDataResult<List<Address>>(_addressDal.GetList().ToList());
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("IAddressService.Get")]//Yeni müşteri eklediği zaman Ön belleği temizleme işlemi. İçersinde IMusteriService.Get olanları Yani başı Get ile başlayanları temizler
        [ValidationAspect(typeof(AddressValidator), Priority = 1)]
        public IResult Add(Address address)
        {
            _addressDal.Add(address);
            return new SuccessResult(Messages.AddressAdded);
        }

        [CacheRemoveAspect("IAddressService.Get")]
        public IResult Delete(Address address)
        {
            _addressDal.Delete(address);
            return new SuccessResult(Messages.AddressDeleted);
        }

        [CacheRemoveAspect("IAddressService.Get")]
        [ValidationAspect(typeof(AddressValidator), Priority = 1)]
        public IResult Update(Address address)
        {
            _addressDal.Update(address);
            return new SuccessResult(Messages.AddressUpdated);
        }
    }
}
