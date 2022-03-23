using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class TelephoneDirectoryManager : ITelephoneDirectoryService
    {
        private ITelephoneDirectoryDal _telephoneDirectoryDal;
        private IAddressDal _addressDal;
        private IAddressService _addressService;

        public TelephoneDirectoryManager(ITelephoneDirectoryDal telephoneDirectoryDal, IAddressDal addressDal)
        {
            _telephoneDirectoryDal = telephoneDirectoryDal;
            _addressDal = addressDal;
        }


        public IDataResult<TelephoneDirectories> GetById(int telephoneDirectoriesId)
        {
            return new SuccessDataResult<TelephoneDirectories>(_telephoneDirectoryDal.Get(telephoneDirectoriesId));
        }


        [SecuredOperation("GetList")]
        [CacheAspect(100)]
        [PerformanceAspect(5)]
        public IDataResult<List<TelephoneDirectories>> GetList()
        {
            return new SuccessDataResult<List<TelephoneDirectories>>(_telephoneDirectoryDal.GetList().ToList());
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ITelephoneDirectoryService.Get")]
        [ValidationAspect(typeof(TelephoneDirectoryValidator), Priority = 1)]
        public IResult Add(TelephoneDirectories telephoneDirectories)
        {

            var result = AddressGetById(telephoneDirectories.AddressId);
            if (result.Data==null)
            {
                return new ErrorResult(Messages.AddressAlreadyExists);
            }
            _telephoneDirectoryDal.Add(telephoneDirectories);
            return new SuccessResult(Messages.TelephoneDirectoryAdded);
        }
        
        public IDataResult<Address> AddressGetById(int addressId)
        {
            return new SuccessDataResult<Address>(_addressDal.Get(addressId));
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ITelephoneDirectoryService.Get")]
        public IResult Delete(TelephoneDirectories telephoneDirectories)
        {
            _telephoneDirectoryDal.Delete(telephoneDirectories);
            return new SuccessResult(Messages.TelephoneDirectoryDeleted);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("ITelephoneDirectoryService.Get")]
        [ValidationAspect(typeof(TelephoneDirectoryValidator), Priority = 1)]
        public IResult Update(TelephoneDirectories telephoneDirectories)
        {
            var result = AddressGetById(telephoneDirectories.AddressId);
            if (result.Data == null)
            {
                return new ErrorResult(Messages.AddressAlreadyExists);
            }

            _telephoneDirectoryDal.Update(telephoneDirectories);
            return new SuccessResult(Messages.TelephoneDirectoryUpdated);
        }
    }
}
