using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebMVC.Models
{
    public class FotografEkle
    {
        public int AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public IFormFile PhotoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string? Fax { get; set; }
        public string? InternalNumber { get; set; }
    }
}
