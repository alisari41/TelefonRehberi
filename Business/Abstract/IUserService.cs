using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetList();
        IDataResult<User> GetById(int userId);
        List<OperationClaim> GetClaims(User user); //Kullanıcın sahip olduğu rolleri getiricem
        IDataResult<List<OperationClaim>> GetClaimsList(User user);
        void Add(User user);
        IResult Delete(User user);
        User GetByMail(string email);//Kullanıcıyı maili vasıtasıyla bulmak istiyorom
    }
}
