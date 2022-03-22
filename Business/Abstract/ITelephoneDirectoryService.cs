using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITelephoneDirectoryService
    {
        IDataResult<TelephoneDirectories> GetById(int telephoneDirectoriesId);
        IDataResult<List<TelephoneDirectories>> GetList();
        IResult Add(TelephoneDirectories telephoneDirectories);
        IResult Delete(TelephoneDirectories telephoneDirectories);
        IResult Update(TelephoneDirectories telephoneDirectories);
    }
}
