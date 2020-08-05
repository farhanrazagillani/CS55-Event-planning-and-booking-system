using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShadiHallBookingSystem.Models;

namespace ShadiHallBookingSystem.Controllers
{
    public class AdminController : Controller
    {

        string connection = ConfigurationManager.ConnectionStrings["DataModel"].ConnectionString;

        DataModel db = new DataModel();


        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            else
            {
              return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public ActionResult Inventory()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");   
            }
        }
        [HttpPost]
        public ActionResult Inventory(hallViewModel h, HttpPostedFileBase Image)
        {
            SqlConnection sq = new SqlConnection(connection);
            string query = "insert into [ShaadiHalls] (Name,Town,Address,Description,Image) values(@Name,@Town,@Address,@Description,@Image)";
            SqlCommand cmd = new SqlCommand(query, sq);
            sq.Open();

            ShaadiHall use = db.ShaadiHalls.Where(x => x.Name == h.Name).FirstOrDefault();

            if (use == null)
            {


                cmd.Parameters.AddWithValue("@Name", h.Name);

                cmd.Parameters.AddWithValue("@Town", h.Town);
                cmd.Parameters.AddWithValue("@Address", h.Address);
                cmd.Parameters.AddWithValue("@Description", h.Description);






                if (Image != null)
                {
                    string filename = Path.GetFileName(Image.FileName);
                    string imagepath = Path.Combine(Server.MapPath("~/Image/") + filename);
                    Image.SaveAs(imagepath);

                }
                cmd.Parameters.AddWithValue("@Image", "~/Image/" + Image.FileName);



                cmd.ExecuteNonQuery();
                var ex = db.ShaadiHalls.Where(x => x.Name == h.Name).FirstOrDefault();

                //adding data of noon shift
                var s = new Shift();
                s.Name = "Noon Time";
                s.Price = h.noonPrice;
                s.S_ID = ex.S_ID;
                db.Shifts.Add(s);

                //adding data of evening shift
                var t = new Shift();
                t.Name = "Evening Time";
                t.Price = h.eveningPrice;
                t.S_ID = ex.S_ID;
                db.Shifts.Add(t);

                var u = new Shift();
                u.Name = "Night Time";
                u.Price = h.nightPrice;
                u.S_ID = ex.S_ID;
                db.Shifts.Add(u);

                var v = new Service();
                v.Wifi = h.Wifi;
                v.musicSystem = h.musicSystem;
                v.Decoration = h.Decoration;
                v.speciaLights = h.speciaLights;
                v.Lights = h.Lights;
                v.Dj = h.Dj;
                v.bikeParking = h.bikeParking;
                v.carParking = h.carParking;
                v.AirCondition = h.AirCondition;
                v.socialMediaPages = h.socialMediaPages;
                v.Seggretion = h.Seggretion;
                v.Catering = h.Catering;
                v.Projector = h.Projector;
                v.stageDecoration = h.stageDecoration;
                v.ladiesWaitress = h.ladiesWaitress;
                v.peopleCapacity = h.peopleCapacity;
                //v.sitting = h.sitting;
                //v.roundTableCapacity = h.roundTableCapacity;
                v.Electricity = h.Electricity;
                v.S_ID = ex.S_ID;
                db.Services.Add(v);
                db.SaveChanges();




                sq.Close();



                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = " This post is already existed";
                return View();
            }
        }


            [HttpGet]
            public ActionResult Records()
            {
                if (Session["Admin"] != null)
                {
                    var u = Convert.ToString(Session["Admin"]);
                    List<ShaadiHall> result = (from c in db.ShaadiHalls                                        
                                         select c).ToList();


                    return View(result);

                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }

            }


        public ActionResult Edit()
        {
            if (Session["Admin"] != null)
            {
                var v = (from c in db.Admins
                         select c).ToList();
                return View(v);
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
        }
        public ActionResult Messages()
        {
            if (Session["Admin"] != null)
            {
                var v = (from c in db.Messages
                         select c).ToList();
                return View(v);
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Admin"] != null)
            {

                var model = (from c in db.Admins
                             where c.A_ID == id
                             select c).SingleOrDefault();


                return PartialView("~/views/admin/_delete.cshtml", model);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult Delete(Admin model)
        {
            Admin f = null;

            f = (from c in db.Admins
                 where c.A_ID == model.A_ID
                 select c).SingleOrDefault();
            db.Admins.Remove(f);
            db.SaveChanges();

            return RedirectToAction("Index");



        }





    }
}
