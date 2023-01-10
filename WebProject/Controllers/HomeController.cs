using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        FlightDBContext db = new FlightDBContext();

        public ActionResult Index(string sortOrder)
        {
            ViewBag.FromSortParm = String.IsNullOrEmpty(sortOrder) ? "From" : "";
            ViewBag.ToSortParm = String.IsNullOrEmpty(sortOrder) ? "To" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";

            var flights = from f in db.Flights
                          select f;

            switch (sortOrder)
            {
                case "From":
                    flights = flights.OrderByDescending(s => s.From);
                    break;
                case "To":
                    flights = flights.OrderByDescending(s => s.To);
                    break;
                case "Price":
                    flights = flights.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    flights = flights.OrderByDescending(s => s.Price);
                    break;
                default:
                    flights = flights.OrderByDescending(s => s.To);
                    break;
            }

            return View(flights.ToList());
        }


        public ActionResult Book()
        {
            return RedirectToAction("FlightSearch", "Flights");
        }
    }
}