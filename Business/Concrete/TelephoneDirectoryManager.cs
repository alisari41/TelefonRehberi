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
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class TelephoneDirectoryManager : ITelephoneDirectoryService
    {
        private ITelephoneDirectoryDal _telephoneDirectoryDal;

        public TelephoneDirectoryManager(ITelephoneDirectoryDal telephoneDirectoryDal)
        {
            _telephoneDirectoryDal = telephoneDirectoryDal;
        }


        public IDataResult<TelephoneDirectories> GetById(int telephoneDirectoriesId)
        {
            return new SuccessDataResult<TelephoneDirectories>(_telephoneDirectoryDal.Get(telephoneDirectoriesId));
        }


        [SecuredOperation("Admin")]
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
            _telephoneDirectoryDal.Add(telephoneDirectories);
            return new SuccessResult(Messages.TelephoneDirectoryAdded);
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
            _telephoneDirectoryDal.Update(telephoneDirectories);
            return new SuccessResult(Messages.TelephoneDirectoryUpdated);
        }
    }
}
