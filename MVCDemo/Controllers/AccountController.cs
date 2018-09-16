using MVCDemo.ViewModel;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace MVCDemo.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel userViewModel)
        {
            WebSecurity.CreateUserAndAccount(userViewModel.UserName, userViewModel.Password);
            Roles.AddUserToRole(userViewModel.UserName, "User");

            using (CoWorkingEntities dbContext = new CoWorkingEntities())
            {
                int userId = WebSecurity.GetUserId(userViewModel.UserName);

                var user = dbContext.Users.Where(x => x.UserId == userId).First();

                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Gender = userViewModel.Gender;
                user.Mobile = userViewModel.Mobile;
                user.Email = userViewModel.Email;

                dbContext.SaveChanges();
            }

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel userViewModel)
        {
            bool isLogin = WebSecurity.Login(userViewModel.UserName, userViewModel.Password);

            if (isLogin)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}