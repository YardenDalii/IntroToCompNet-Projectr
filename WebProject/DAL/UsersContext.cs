using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebProject.Models;

namespace WebProject.DAL
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("UsersContext")
        {
        }

        public DbSet<CreditCard> Cards { get; set; }
        public DbSet<UserB> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}