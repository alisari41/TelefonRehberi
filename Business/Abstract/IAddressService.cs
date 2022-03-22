using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAddressService
    {
        IDataResult<Address> GetById(int addressId);//Data başarlımı oldu başarısız mı onlarada bakıcam IDataResult ile
        IDataResult<List<Address>> GetList();
        IResult Add(Address address);
        IResult Delete(Address address);
        IResult Update(Address address);

    }
}
