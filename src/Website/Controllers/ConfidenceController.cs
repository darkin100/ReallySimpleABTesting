using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class ConfidenceController : Controller
    {
        public ActionResult Index()
        {
            var model = new ConfidenceCalculator(182d, 35d);
            
            model.AddTest(182,35);
            model.AddTest(180,45);
            model.AddTest(189,28);
            model.AddTest(188,61);

            return View(model);
        }
    }
}
