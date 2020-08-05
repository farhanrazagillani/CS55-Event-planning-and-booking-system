using ShadiHallBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShadiHallBookingSystem.Controllers
{
    public class LoginController : Controller
    {
        
        DataModel db = new DataModel();

        public ActionResult Registration()
        {




            return View();
        }
        [HttpPost]
        //public ActionResult Registration(Admin a)
        //{

        //    Admin us = db.Admins.Where(x => x.Email == a.Email).FirstOrDefault();
        //    if (us == null)
        //    {
        //        var v = new Admin();
        //        v.Name = a.Name;
        //        v.Email = a.Email;
        //        v.Password = a.Password;

        //        db.Admins.Add(v);
        //        db.SaveChanges();
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        ViewBag.msg = " The user of this email_id is already existed";
        //        return View();
        //    }



        //}





        public ActionResult Login()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                var username = reqCookies["Username"].ToString();
                var password = reqCookies["Password"].ToString();
                Admin us = db.Admins.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                if (us != null)
                {
                    Session["Admin"] = us.A_ID;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }

            }

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel a)
        {
            Admin us = db.Admins.Where(x => x.Username == a.Username && x.Password == a.Password).FirstOrDefault();
            if (us != null)
            {
                Session["Admin"] = us.A_ID;

                if (a.RememberMe)
                {
                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["Username"] = us.Username;
                    userInfo["Password"] = us.Password;

                    userInfo.Expires.Add(new TimeSpan(7, 0, 0, 0));
                    Response.Cookies.Add(userInfo);


                }
                var admin = new Admin();
                admin.Username = a.Username;
                admin.Password = a.Password;
                db.Admins.Add(admin);
                db.SaveChanges();


                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.mesg = "Incorrect Email and Password";
                Session["msg"] = ViewBag.mesg;
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["Admin"] = null;
            HttpCookie temp = Request.Cookies["userInfo"];
            if (temp != null)
            {
                Response.Cookies.Remove("userInfo");


            }
            return RedirectToAction("login");
        }


    }
}