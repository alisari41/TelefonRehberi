﻿using System;
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
        List<OperationClaim> GetClaims(User user); //Kullanıcın sahip olduğu rolleri getiricem
        void Add(User user);
        User GetByMail(string email);//Kullanıcıyı maili vasıtasıyla bulmak istiyorom
    }
}
