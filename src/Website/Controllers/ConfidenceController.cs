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
            var model = new ConfidenceCalculator(47d, 0d);
            
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Index(double controlVisits,double controlConversions,double testVisits,double testConversions)
        {
            var model = new ConfidenceCalculator(controlVisits, controlConversions);
            
            model.AddTest(testVisits,testConversions);

            return View(model);
        }
    }
}
