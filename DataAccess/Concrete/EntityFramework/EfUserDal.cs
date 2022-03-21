﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, TelefonRehberiContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            //Bir kullanıcının rollerini çekmek istiyorum.
            //Beni bunu yapmam için join işlemi yapmam lazım Yani 2 tabloyu birleştirmem lazım.

            using (var context = new TelefonRehberiContext())
            {
                //Gelen User bilgilerinin join işlemleri ile rollerini listeledim
                var result = from operationCalaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationCalaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id //sınırlandırma yaptım
                             select new OperationClaim
                             {
                                 //Burdan bir operationclaim rol listesi döndürcem
                                 Id = operationCalaim.Id,
                                 Name = operationCalaim.Name
                             };
                return result.ToList();
            }
        }
    }
}
