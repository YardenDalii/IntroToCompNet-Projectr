using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using WebProject.Models;
using System.Data.Entity.Migrations;

namespace WebProject.Controllers
{
    public class FlightsController : Controller
    {
        private FlightDBContext db = new FlightDBContext();

        public static int Seat;
        public static Flight F;

        public static Flight F1, F2;

        // GET: Flights
        public ActionResult Index()
        {
            return View(db.Flights.ToList());
        }

        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,From,To,DepTime,ArrTime,SeatCap,Price")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,From,To,DepTime,ArrTime,SeatCap,Price")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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

        public ActionResult FlightSearch()
        {

            return View();
        }


        public ActionResult FlightSubmit1([Bind(Include = "ID,From,To,DepTime,ArrTime,SeatCap,Price")] Flight flight)
        {
           
            Seat = flight.SeatCap;
            
            List<Flight> QueryFlights = new List<Flight>();
            
            foreach (Flight f in db.Flights)
            {
                if (flight.From == f.From && flight.To == f.To && flight.DepTime.Date == f.DepTime.Date && DateTime.Compare(DateTime.Today, flight.DepTime) <= 0 && flight.Price >= f.Price)
                {
                    QueryFlights.Add(f);
                }
            }
            
            return View("SearchResult1", QueryFlights);
        }

        public ActionResult BookNow1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Booking1", flight); //Need to be redirected to payment viw that user choose number of seats
        }


        public ActionResult Booking1([Bind(Include = "ID,From,To,DepTime,ArrTime,SeatCap,Price")] Flight flight)
        {
            Flight f;
            if (ModelState.IsValid)
            {
                if (flight.SeatCap >= Seat)
                {
                    f = new Flight()
                    {
                        ID = flight.ID,
                        From = flight.From,
                        To = flight.To,
                        DepTime = flight.DepTime,
                        ArrTime = flight.ArrTime,
                        SeatCap = (flight.SeatCap - Seat),
                        Price = flight.Price
                    };
                    db.Flights.AddOrUpdate(f);
                    db.SaveChanges();

                    F = new Flight()
                    {
                        ID = f.ID,
                        From = f.From,
                        To = f.To,
                        ArrTime = f.ArrTime,
                        DepTime = f.DepTime,
                        SeatCap = f.SeatCap,
                        Price = f.Price
                    };

                    return RedirectToAction("Payment");
                }

                else
                {
                    MessageBox.Show("No Seats Available.");
                    return View("FlightSearch", flight);
                }
            }

            return View("FlightSearch", flight);
        }

        public ActionResult Payment()
        {
            return View();
        }
        
        public ActionResult Proccess([Bind(Include = "ID,Email,FullName,id_number")] UserB user)
        {

            if (ModelState.IsValid)
            {
                return RedirectToAction("Proccess", "CreditCards", user);
            }
            return View("Index");
        }

        public ActionResult Recipt([Bind(Include = "ID,Email,FullName,id_number")] UserB user)
        {
            Ticket t1 = new Ticket()
            {
                Email = user.Email,
                FullName = user.FullName,
                id = user.id_number
            };



            if (F2 == null)
            {
                BookedTicket bt = new BookedTicket()
                {
                    Person = t1,
                    Flight = F
                };
                return View("Work", bt);
            }

            BookedTicket2 bt2 = new BookedTicket2()
            {
                Person = t1,
                Flight1 = F1,
                Flight2 = F2
            };
            
            return View("Work2", bt2);
        }


        public ActionResult FlightSubmit2([Bind(Include = "ID,From,To,DepTime,ArrTime,SeatCap,Price")] Flight flight)
        {

            Seat = flight.SeatCap;

            List<Flight> QueryFlights1 = new List<Flight>();
            List<Flight> QueryFlights2 = new List<Flight>();

            foreach (Flight f in db.Flights)
            {
                if (flight.From == f.From && flight.To == f.To && flight.DepTime.Date == f.DepTime.Date && DateTime.Compare(DateTime.Today, flight.DepTime) <= 0 && flight.Price >= f.Price)
                {
                    QueryFlights1.Add(f);
                }

                if(flight.From == f.To && flight.To == f.From && flight.ArrTime.Date == f.DepTime.Date && DateTime.Compare(DateTime.Today, flight.ArrTime) <= 0 && flight.Price >= f.Price)
                {
                    QueryFlights2.Add(f);
                }
            }

            TwoWayF Queries = new TwoWayF()
            {
                FromList = QueryFlights1,
                ToList = QueryFlights2
            };

            return View("SearchResult2", Queries);

        }

        public ActionResult BookNow2(TwoWayF twf)
        {
            MessageBox.Show(Request.Form["Book1"]);

            int inbound, outbound;
            inbound = Int32.Parse(Request.Form["Book1"]);
            outbound = Int32.Parse(Request.Form["Book2"]);


            Flight inF = db.Flights.Find(inbound);
            Flight outF = db.Flights.Find(outbound);

            Flight f1,f2;

            if (inF.SeatCap >= Seat && outF.SeatCap >= Seat)
            {
                f1 = new Flight()
                {
                    ID = inF.ID,
                    From = inF.From,
                    To = inF.To,
                    DepTime = inF.DepTime,
                    ArrTime = inF.ArrTime,
                    SeatCap = (inF.SeatCap - Seat),
                    Price = inF.Price
                };

                f2 = new Flight()
                {
                    ID = outF.ID,
                    From = outF.From,
                    To = outF.To,
                    DepTime = outF.DepTime,
                    ArrTime = outF.ArrTime,
                    SeatCap = (outF.SeatCap - Seat),
                    Price = outF.Price
                };

                db.Flights.AddOrUpdate(f1);
                db.SaveChanges();
                db.Flights.AddOrUpdate(f2);
                db.SaveChanges();

                F1 = new Flight()
                {
                    ID = f1.ID,
                    From = f1.From,
                    To = f1.To,
                    ArrTime = f1.ArrTime,
                    DepTime = f1.DepTime,
                    SeatCap = f1.SeatCap,
                    Price = f1.Price
                };

                F2 = new Flight()
                {
                    ID = f2.ID,
                    From = f2.From,
                    To = f2.To,
                    ArrTime = f2.ArrTime,
                    DepTime = f2.DepTime,
                    SeatCap = f2.SeatCap,
                    Price = f2.Price
                };

                return RedirectToAction("Payment");
            }

            else
            {
                MessageBox.Show("No Seats Available.");
                return View("FlightSearch");
            }

        }

    }
}
