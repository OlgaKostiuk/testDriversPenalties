using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataFines;
using System.Web.Helpers;
using System.Web.Security;

namespace AspNetFines.Controllers
{
    public class UserController : Controller
    {
        public ActionResult SignUp()
        {
            ViewBag.Brands = GetBrands();
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user, int[] brands)
        {
            User addedUser = AddUser(user, brands);
            if(addedUser != null)
                return RedirectToAction("SignIn", "User");
            return RedirectToAction("SignUp", "User");
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string email, string password)
        {
            var user = UserVerification(email, password);

            if (user == null)
                return View();

            FormsAuthentication.SetAuthCookie(user.Email, true);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }






        //should be in Repository
        private List<Brand> GetBrands()
        {
            using (FinesContext context = new FinesContext())
            {
                return context.Brand.ToList();
            }
        }

        private Brand GetBrandById(int id)
        {
            using (FinesContext context = new FinesContext())
            {
                return context.Brand.Find(id);
            }
        }
        private User UserVerification(string email, string password)
        {
            using (FinesContext context = new FinesContext())
            {
                var user = GetUserByEmail(email);

                return user != null && Crypto.VerifyHashedPassword(user.Password, password) ? user : null;
            }

        }
        private User GetUserByEmail(string email)
        {
            using (FinesContext context = new FinesContext())
            {
                return context.User.FirstOrDefault(x => x.Email == email);
            }   
        }
        private bool IsEmailUsed(string email)
        {
            using (FinesContext context = new FinesContext())
            {
                return context.User.Any(x => x.Email == email);
            }
        }
        private User AddUser(User user, int[] brands)
        {
            if (IsEmailUsed(user.Email)) return null;
            user.Password = Crypto.HashPassword(user.Password);
            using (FinesContext context = new FinesContext())
            {
                context.User.Add(user);
                context.SaveChanges();
                if(brands != null)
                {
                    foreach (var id in brands)
                    {
                        UsersBrand userBrands = new UsersBrand()
                        {
                            UserId = user.Id,
                            BrandId = id
                        };
                        context.UsersBrand.Add(userBrands);
                    }
                    context.SaveChanges();
                }
            }
            return user;
        }
    }
}