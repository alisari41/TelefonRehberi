using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PhoneAddressManager :IPhoneAddressService
    {
        private IPhoneAddressDal _phoneAddressDal;

        public PhoneAddressManager(IPhoneAddressDal phoneAddressDal)
        {
            _phoneAddressDal = phoneAddressDal;
        }

        public IDataResult<List<Deneme>> GetList()
        {
            return new SuccessDataResult<List<Deneme>>(_phoneAddressDal.GetList().ToList());
        }
    }
}
