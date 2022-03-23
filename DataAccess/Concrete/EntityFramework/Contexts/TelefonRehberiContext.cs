using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class TelefonRehberiContext : DbContext
    { //Context = Nesnelerimiz ile veri tabanımızın iletişimi kuran nesnedir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=DESKTOP-8R3FVLB\SQLEXPRESS;Database=TelefonRehberi;Trusted_Connection=true;");
        }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<TelephoneDirectories> TelephoneDirectories { get; set; }
        public DbSet<Address> Address { get; set; }


    }
}
