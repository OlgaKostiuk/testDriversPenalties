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
    public class HistoryController : Controller
    {
        // GET: History
        [Authorize]
        public ActionResult History()
        {
            List<HistoryViewModel> list = GetUserHistory();
            return View(list);
        }

        [Authorize]
        public ActionResult Details(int Id)
        {
            HistoryViewModel history = GetHistoryViewModelByHistoryId(Id);
            return View(history);
        }

        [Authorize]
        public ActionResult Pay(int Id)
        {
            PayHistoryFine(Id);

            HistoryViewModel history = GetHistoryViewModelByHistoryId(Id);
            return View("Details", history);
        }






        //should be in Repository
        private void PayHistoryFine(int historyId)
        {
            using (FinesContext context = new FinesContext())
            {
                History history = context.History.Find(historyId);
                history.State = true;
                context.SaveChanges();
            }
        }
        private HistoryViewModel GetHistoryViewModelByHistoryId(int historyId)
        {
            using (FinesContext context = new FinesContext())
            {
                History history = context.History.Find(historyId);
                Brand brand = GetBrandByUserBrandId(history.UsersBrandId);
                Fine fine = GetFineById(history.FineId);
                return GetHistoryViewModel(history, brand, fine);
            }
        }
        private List<HistoryViewModel> GetUserHistory()
        {
            List<HistoryViewModel> list = new List<HistoryViewModel>();
            User user = GetUserByEmail(User.Identity.Name);

            using (FinesContext context = new FinesContext())
            {
                List<UsersBrand> userBrands = context.UsersBrand.Where(x => x.UserId == user.Id).ToList();
                foreach (var item in userBrands)
                {
                    list.AddRange(context.History.Where(x => x.UsersBrandId == item.Id).ToList().Select(x => GetHistoryViewModel(x, GetBrandById(item.BrandId), GetFineById(x.FineId))));
                }       
            }
            return list;
        }
        private HistoryViewModel GetHistoryViewModel(History history, Brand brand, Fine fine)
        {
            return new HistoryViewModel()
            {
                HistoryId = history.Id,
                Brand = brand.BrandName,
                Desctiption = fine.Description,
                Price = fine.Price,
                State = history.State
            };
        }
        private Fine GetFineById(int id)
        {
            using (FinesContext context = new FinesContext())
            {
                return context.Fine.Find(id);
            }
        }
        private Brand GetBrandByUserBrandId(int id)
        {
            using (FinesContext context = new FinesContext())
            {
                UsersBrand userBrand = context.UsersBrand.Find(id);
                return context.Brand.FirstOrDefault(x => x.Id == userBrand.BrandId);
            }
        }
        private Brand GetBrandById(int id)
        {
            using (FinesContext context = new FinesContext())
            {
                return context.Brand.Find(id);
            }
        }
        private User GetUserByEmail(string email)
        {
            using (FinesContext context = new FinesContext())
            {
                return context.User.FirstOrDefault(x => x.Email == email);
            }
        }
    }
}