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
        public ActionResult Registration(Admin a)
        {

            Admin us = db.Admins.Where(x => x.Email == a.Email).FirstOrDefault();
            if (us == null)
            {
                var v = new Admin();
                
                v.Email = a.Email;
                v.Password = a.Password;
               
                db.Admins.Add(v);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.msg = " The user of this email_id is already existed";
                return View();
            }



        }
        public ActionResult signIn()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                var email = reqCookies["Email"].ToString();
                var password = reqCookies["Password"].ToString();
                User us = db.Users.Where(x => x.Email_ID == email && x.Password == password).FirstOrDefault();
                if (us != null)
                {
                    Session["User"] = us.Email_ID;

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.mesg = Session["msg"];
                    return RedirectToAction("signIn", "Login");
                }

            }

            return View();
        }




        [HttpPost]
        public ActionResult signIn(LoginViewModel a)
        {
            User us = db.Users.Where(x => x.Email_ID == a.Email && x.Password == a.Password).FirstOrDefault();
            if (us != null)
            {
                Session["User"] =Convert.ToString(us.Email_ID);

                if (a.RememberMe)
                {
                    HttpCookie user = new HttpCookie("userInfo");
                    user["Email"] = us.Email_ID;
                    user["Password"] = us.Password;

                    user.Expires.Add(new TimeSpan(7, 0, 0, 0));
                    Response.Cookies.Add(user);


                }

                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.mesg = "Incorrect Email and Password";
                Session["msg"] = ViewBag.mesg;
                return RedirectToAction("signIn");
            }
        }











        public ActionResult Login()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                var email = reqCookies["Email"].ToString();
                var password = reqCookies["Password"].ToString();
                Admin us = db.Admins.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                if (us != null)
                {
                    Session["Admin"] = us.A_ID;

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.mesg = Session["msg"];
                    return RedirectToAction("Login","Login");
                }

                }

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel a)
        {
            Admin us = db.Admins.Where(x => x.Email == a.Email && x.Password == a.Password).FirstOrDefault();
            if (us != null)
            {
                Session["Admin"] = us.A_ID;

                if (a.RememberMe)
                {
                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["Email"] = us.Email;
                    userInfo["Password"] = us.Password;

                    userInfo.Expires.Add(new TimeSpan(7, 0, 0, 0));
                    Response.Cookies.Add(userInfo);


                }

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
            return RedirectToAction("Index","Home");
        }
        public ActionResult SignOut()
        {
            Session["User"] = null;
            HttpCookie temp = Request.Cookies["userInfo"];
            if (temp != null)
            {
                Response.Cookies.Remove("userInfo");

                
            }
            return RedirectToAction("Index","Home");
        }


    }
}