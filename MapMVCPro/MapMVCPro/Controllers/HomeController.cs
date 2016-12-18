using MapMVCPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapMVCPro.Controllers
{
    public class HomeController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        public ActionResult Index(string searchString)
        {
            var locations = from l in db.Locations
                            select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(l => l.Title.Contains(searchString));
            }
            return View(locations);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Quote form our CEO ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contace We";

            return View();
        }
        public JsonResult GetAllLocation()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Locations.OrderBy(a => a.Title).ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult GetMarkerInfo(int locationID)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                Location l = null;
                l = dc.Locations.Where(a => a.LocationID.Equals(locationID)).FirstOrDefault();
                return new JsonResult { Data = l, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}