using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ShadiHallBookingSystem.Models;
using Stripe;

namespace ShadiHallBookingSystem.Controllers
{
    public class HomeController : Controller
    {

        private string ClientsId = "pk_test_4GsAFU8K9dobIsIuevGBZuEQ00zNfntuam";




        string connection = ConfigurationManager.ConnectionStrings["DataModel"].ConnectionString;

        DataModel db = new DataModel();



        public ActionResult Index()
        {
            ViewBag.charge = Convert.ToInt32(Session["charge"]);
            ViewBag.message = Session["msg"];
           


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(Message m)
        {
            var message = new Message();
            message.name = m.name;
            message.email = m.email;
            message.subject = m.subject;
            message.message = m.message;
            db.Messages.Add(message);
            db.SaveChanges();


            return View();
        }
        public ActionResult Booking()
        {
           
            //id for getting the shift object 
            var hamza = Convert.ToInt32(Session["Shift_id"]);
            var v = db.Shifts.Where(x => x.Sh_ID == hamza).FirstOrDefault();

            var shadi = db.Shifts.Where(x => x.Sh_ID == hamza).FirstOrDefault().ShaadiHall;
            var servicess = db.Services.Where(x => x.S_ID == shadi.S_ID).FirstOrDefault();
            ViewBag.service = servicess;



           
            return View(v);
        }
        [HttpPost]
         public ActionResult Booking(bookingViewModel b)
        {
            var price = Convert.ToInt32(Session["price"]);

            var total = (b.Guests) * (price) * ((b.ToDate.Day) - (b.FromDate.Day));


           

            var charges = new ChargeService();


            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = (total / 160) * 100,//charge in cents

                Description = "Sample Charge",
                Currency = "usd",
                Customer = Convert.ToString(Session["customer_id"]),


                ReceiptEmail = Convert.ToString(Session["stripeEmail"]),


            });







            var u = new User();
            u.Name = b.Name;
            u.Email_ID = b.Email;
            u.Address = b.Address;
            u.Password = b.Password;
            db.Users.Add(u);
            db.SaveChanges();
            var v = new Booking();


            v.FromDate = b.FromDate;
            v.ToDate = b.ToDate;
            v.Total = total;
            v.U_ID = u.U_ID;
            v.Sh_ID = Convert.ToInt32(Session["id"]);
            db.Bookings.Add(v);
            db.SaveChanges();


            if (charge.Status == "succeeded")
            {
                var a = ((charge.Amount*160)/100);
               Session["charge"] =Convert.ToString(Convert.ToInt32 (a));
                return RedirectToAction("Index");
            }
            else
            {
                var a = "Your payment is not done due to some reasons,so kindly do it again";
                Session["msg"] = a;
                return RedirectToAction("Index");

                

            }
        }


        public ActionResult Halls()
        {
            var v = (from c in db.ShaadiHalls

                     select c).ToList();


            return View(v);

            
        }
        public ActionResult hallDetails(int id)
        {

            var v = db.Shifts.Where(x => x.Sh_ID == id).FirstOrDefault();

           
            var data = db.Services.Where(x => x.S_ID == v.S_ID).FirstOrDefault();

            ViewBag.DataPoints = data;
            return View(v);

        }
        [HttpPost]
        public ActionResult hallDetails(string stripeEmail, string stripeToken, int id)
        {

            var customers = new CustomerService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,

                Source = stripeToken,

            });

            //add price and of shift in the session

            var v = db.Shifts.Where(x => x.Sh_ID == id).FirstOrDefault();

            Session["price"] = Convert.ToString(Convert.ToInt32(v.Price));
            Session["id"] = Convert.ToString(Convert.ToInt32(v.Sh_ID));





            Session["stripeEmail"] = customer.Email;
            var a = customer.Id;
            Session["customer_id"] = Convert.ToString(a);


            Session["Shift_id"] = Convert.ToString(Convert.ToInt32(id));



            //Session["Sh_id"] = id;
            return RedirectToAction("Booking");

        }



    }
}