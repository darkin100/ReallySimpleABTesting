using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private IAdvert _advert;
        private string _testname;

        public HomeController(IAdvert advert)
        {
            _advert = advert;
            _testname = ConfigurationManager.AppSettings["testname"];
        }

        public ActionResult Index()
        {
            ViewBag.Message = _advert.Text;

            return View();
        }

        public ActionResult StartTestA()
        {
            var myCookie = new HttpCookie(_testname, "A");

            Response.AppendCookie(myCookie);

            return RedirectToAction("Index");
        }

        public ActionResult StartTestB()
        {
            var myCookie = new HttpCookie(_testname, "B");

            Response.AppendCookie(myCookie);

            return RedirectToAction("Index");
        }

        public ActionResult ClearCookies()
        {
            var myCookie = new HttpCookie(_testname);
            myCookie.Expires = DateTime.Now.AddDays(-1D);

            Response.AppendCookie(myCookie);

            return RedirectToAction("Index");
        }
    }
}