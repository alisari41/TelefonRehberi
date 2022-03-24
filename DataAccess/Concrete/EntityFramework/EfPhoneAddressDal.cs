using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPhoneAddressDal : EfEntityRepositoryBase<TelephoneDirectories, TelefonRehberiContext>, IPhoneAddressDal
    {
        public IList<Deneme> GetList()
        {
            using (var context = new TelefonRehberiContext())
            {
                //Gelen User bilgilerinin join işlemleri ile rollerini listeledim
                var result = from telephoneDirectories in context.TelephoneDirectories
                             join address in context.Address on telephoneDirectories.AddressId equals address.Id
                             select new Deneme()
                             {
                                 Id = telephoneDirectories.Id,
                                 FirstName = telephoneDirectories.FirstName,
                                 LastName = telephoneDirectories.LastName,
                                 Title = telephoneDirectories.Title,
                                 Email = telephoneDirectories.Email,
                                 PhotoUrl = telephoneDirectories.PhotoUrl,
                                 PhoneNumber = telephoneDirectories.PhoneNumber,
                                 Fax = telephoneDirectories.Fax,
                                 InternalNumber = telephoneDirectories.InternalNumber,
                                 Mahalle = address.Mahalle,
                                 Cadde = address.Cadde,
                                 Sokak = address.Sokak,
                                 BinaNo = address.BinaNo,
                                 İlce = address.İlce,
                                 İl = address.İl,
                                 PostaKodu = address.PostaKodu
                             };
                return result.ToList(); }
        }
    }
}
