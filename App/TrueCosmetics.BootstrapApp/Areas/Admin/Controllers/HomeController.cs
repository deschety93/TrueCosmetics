using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrueCosmetics.BootstrapApp.Areas.Admin.Models;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Controllers
{
    public class HomeController : AdminController<Order>
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlotCharts()
        {
            return View("FlotCharts");
        }

        public ActionResult MorrisCharts()
        {
            return View("MorrisCharts");
        }

        public ActionResult Tables()
        {
            return View("Tables");
        }

        public ActionResult Forms()
        {
            return View("Forms");
        }

        public ActionResult Panels()
        {
            return View("Panels");
        }

        public ActionResult Buttons()
        {
            return View("Buttons");
        }

        public ActionResult Notifications()
        {
            var result = ChangeNotification.AllNotifications.Take(10);
            return View(result);
        }

        public async Task<ActionResult> OrderStatus()
        {
            IDictionary<Status, int> result = await Set.All()
                .Include(x => x.OrderStatus)
                .GroupBy(x => x.OrderStatus.Status).ToDictionaryAsync(x => x.Key, y => y.Count());

            return View(result);
        }

        public async Task<ActionResult> OrderAddresses()
        {
            var result = (await Set.All()
                .Include(x => x.OrderDetails.Select(y => y.Product))
                .Include(x => x.UserAddress)
                .ToListAsync())
                .Select(x => new { City = x.UserAddress.City, Amount = x.OrderDetails.Sum(y => y.Quantity * y.Product.Price) })
                .GroupBy(x => x.City)
                .ToDictionary(x => x.Key, y => y.Sum(z => z.Amount))
                .Select(x => new { label = x.Key, value = x.Value});

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Typography()
        {
            return View("Typography");
        }

        public ActionResult Icons()
        {
            return View("Icons");
        }

        public ActionResult Grid()
        {
            return View("Grid");
        }

        public ActionResult Blank()
        {
            return View("Blank");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        public async Task<ActionResult> OrderData(int range)
        {
            DateTime period = DateTime.Now.AddDays(-range);
            var result = (await Set.All()
                .Include(x => x.OrderStatus)
                .Where(x => x.OrderDate >= period)
                .ToListAsync())
                .Select(x => new { Date = x.OrderDate.Value , Status = x.OrderStatus.Status})
                .GroupBy(x => x.Date)
                .OrderBy(x => x.Key)
                .Select(x => new {
                    Period = x.Key.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                    DeliveredCount = x.Count(y => y.Status == Status.Shipped),
                    AcceptedCount = x.Count(y => y.Status == Status.Accepted),
                    PendingCount = x.Count(y => y.Status == Status.Pending),
                    RejectedCount = x.Count(y => y.Status == Status.Rejected)
                }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ProductsData()
        {
            DateTime month = DateTime.Now.AddMonths(-1);
            DateTime prevMonth = DateTime.Now.AddMonths(-2);
            var result = (await Set.All()
                .Include(x => x.OrderDetails.Select(y => y.Product))
                .Where(x => x.OrderDate >= prevMonth)
                .SelectMany(x => x.OrderDetails)
                .ToListAsync())
                .GroupBy(x => x.Product.Name)
                .OrderBy(x => x.Key)
                .Select(x => new
                {
                    Product = x.Key,
                    ThisMonth = x.Where(y => y.Order.OrderDate >= month).Sum(z => z.Quantity),
                    PreviousMonth = x.Where(y => y.Order.OrderDate >= prevMonth && y.Order.OrderDate < month).Sum(z => z.Quantity)
                }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}