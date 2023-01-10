using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebProject.Models;

namespace WebProject.DAL
{
    public class UserInitiallizer : System.Data.Entity.DropCreateDatabaseIfModelChanges<UsersContext>
    {
        protected override void Seed(UsersContext context)
        {
            var users = new List<UserB>
            {
            new UserB{Email="yarden1@email.com",FullName="Yarden Dali",id_number="207220013"},
            new UserB{Email="yarden2@email.com",FullName="Yarden Dali",id_number="207220013"},
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var cards = new List<CreditCard>
            {
                new CreditCard{UserBID=1, CardNumber="1234123412341234", ExpDate="1234", CVV="123"},
            };

            cards.ForEach(c => context.Cards.Add(c));
            context.SaveChanges();

        }


    }

}