using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using WebProject.DAL;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class CreditCardsController : Controller
    {
        private UsersContext db = new UsersContext();

        public static int id;
        // GET: CreditCardsB
        public ActionResult Index()
        {
            return View(db.Cards.ToList());
        }

        // GET: CreditCardsB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.Cards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // GET: CreditCardsB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreditCardsB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CardNumber,ExpDate,CVV")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                db.Cards.Add(creditCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creditCard);
        }

        // GET: CreditCardsB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.Cards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCardsB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CardNumber,ExpDate,CVV")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creditCard);
        }

        // GET: CreditCardsB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.Cards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCardsB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = db.Cards.Find(id);
            db.Cards.Remove(creditCard);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Proccess([Bind(Include = "ID,Email,FullName,id_number")] UserB user)
        {
            int id_;
            foreach (UserB item in db.Users)
            {
                if (item.Email == user.Email)
                {
                    id_ = item.ID;
                    foreach (CreditCard c in db.Cards)
                    {
                        if (id_ == c.UserBID)
                        {
                            id = item.ID;
                            return View("HavePayMethod", item); //already have pay method
                        }

                    }

                    id = item.ID;
                    return RedirectToAction("Pay"); //dont have pay method

                }
            }

            int i = db.Users.Max(u => u.ID);
            id = i+1;
            user.ID = id;
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            return RedirectToAction("pay"); //user doesnt exist. send with user

        }


        public ActionResult Pay()
        {

            return View();
        }

        public ActionResult SavedPay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.Cards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Payment",creditCard);
        }


        public ActionResult Payment([Bind(Include = "ID,UserBID,CardNumber,ExpDate,CVV")] CreditCard creditCard)
        {
            UserB u = db.Users.Find(id);
            bool save = false;
            string saveValue = "";
            
            if (!string.IsNullOrEmpty(Request.Form["save"]))
            {
                save = true;
                if (u == null)
                {
                    return HttpNotFound();
                }

                CreditCard newCard = new CreditCard()
                {
                    UserBID = u.ID,
                    CardNumber = creditCard.CardNumber,
                    ExpDate = creditCard.ExpDate,
                    CVV = creditCard.CVV
                };

                
                var card_ = db.Cards.Where(c => c.UserBID == newCard.UserBID && c.CardNumber == newCard.CardNumber).SingleOrDefault();
                if(card_ == null)
                {
                    db.Cards.AddOrUpdate(newCard);
                }

                db.SaveChanges();

            }

            if (save)
            {
                saveValue = Request.Form["save"];
            }


            return RedirectToAction("Recipt", "Flights",u);
        }

    }
}

