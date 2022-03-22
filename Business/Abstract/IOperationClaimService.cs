using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<OperationClaim> GetById(int operationClaimId);//Data başarlımı oldu başarısız mı onlarada bakıcam IDataResult ile
        IDataResult<List<OperationClaim>> GetList();
        IResult Add(OperationClaim operationClaim);// Data döndürmek istemiyorum.Başarılı mı oldum başarısız mı onlara bakmak istiyorum.
        IResult Delete(OperationClaim operationClaim);
        IResult Update(OperationClaim operationClaim);

    }
}
