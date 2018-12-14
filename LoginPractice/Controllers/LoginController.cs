using LoginPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginPractice.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        private PrscticeEntities db = new PrscticeEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginForm()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Autherize(LoginPractice.Models.UsersDetail userModel)

        {

            using (PrscticeEntities db = new PrscticeEntities())
            {
                var userDetails = db.UsersDetails.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Username OR Password,Please Sign Up First or use Forget Password";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("Loggedin", "Home");
                }
            }

        }
        public ActionResult  Register()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,Name,UserName,E_mail,Password")] UsersDetail usersDetail)
        {
            if (ModelState.IsValid)
            {
                db.UsersDetails.Add(usersDetail);
                db.SaveChanges();
                return RedirectToAction("Success");
            }

            return View(usersDetail);
        }
    }
}