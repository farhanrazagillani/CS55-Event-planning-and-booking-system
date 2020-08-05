using ShadiHallBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShadiHallBookingSystem.Controllers
{
    public class UserController : Controller
    {
        DataModel db = new DataModel();

        // GET: User
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignIn", "Login");
            }
        }
        public ActionResult userDetails()
        {
            if (Session["User"] != null)
            {
                var temp1 = Convert.ToString(Session["User"]);
                var user = db.Users.Where(x => x.Email_ID == temp1).FirstOrDefault();


                return View(user);
            }
            else
            {
                return RedirectToAction("SignIn", "Login");
            }
        }




        public ActionResult bookingDetails()
        {
            if (Session["User"] != null)
            {
                var temp = Convert.ToString(Session["User"]);
                var booking = (from c in db.Bookings
                               where c.User.Email_ID == temp
                               select c).ToList();
                return View(booking);
            }
            else
            {
                return RedirectToAction("SignIn", "Login");

            }
        }
        public ActionResult editDetails()
        {

            if (Session["User"] != null)
            {
                ViewBag.error = Convert.ToString(TempData["error"]);



                var temp = Convert.ToString(Session["User"]);
                var booking = (from c in db.Bookings
                               where c.User.Email_ID == temp
                               select c).ToList();
                return View(booking);
            }
            else
            {
                return RedirectToAction("SignIn", "Login");
            }
        }


        public ActionResult edit(int id)
        {

            var model = (from c in db.Bookings
                         where c.B_ID == id
                         select c).SingleOrDefault();


            return PartialView("~/views/User/_edit.cshtml", model);

        }
        [HttpPost]
        public ActionResult edit(Booking model, int id)
        {
            var a = db.Bookings.Where(x => x.B_ID == id).SingleOrDefault();
            a.B_ID = id;

            //check the difference between the two dates

            var from = a.FromDate;
            var to = a.ToDate;

            var diff = to - from;

            //check new difference of dates

            var newFrom = model.FromDate;
            var newTo = model.ToDate;

            var diff2 = newTo - newFrom;

            if (diff2 == diff)
            {
                a.FromDate = model.FromDate;
                a.ToDate = model.ToDate;
                a.Total = a.Total;
                a.Sh_ID = a.Sh_ID;
                a.U_ID = a.U_ID;
                db.SaveChanges();


                TempData["error"] = "The Booking data is edited";


                return RedirectToAction("bookingDetails");
            }
            else
            {
                a.FromDate = a.FromDate;
                a.ToDate = a.ToDate;
                a.Total = a.Total;
                a.Sh_ID = a.Sh_ID;
                a.U_ID = a.U_ID;
                db.SaveChanges();

                TempData["error"] = " Not edited beacause total days of booking cannot be changed";
                return RedirectToAction("editDetails");
            }
           

        }
    }
}