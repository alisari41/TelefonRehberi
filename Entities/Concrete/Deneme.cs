using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Deneme : IEntity
    {
        public int  Id { get; set; }
        public string Mahalle { get; set; }
        public string Cadde { get; set; }
        public string Sokak { get; set; }
        public string BinaNo { get; set; }
        public int Kat { get; set; }
        public string İlce { get; set; }
        public string İl { get; set; }
        public int PostaKodu { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string? Fax { get; set; }
        public string? InternalNumber { get; set; }
    }
}
