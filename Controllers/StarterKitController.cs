using Microsoft.AspNetCore.Mvc;

namespace POSBarasaki.Controllers
{
    public class StarterKitController : Controller
    {
        public IActionResult Layoutlight()
        {
            ViewBag.Bodyclass = "";
            return View();
        }

        public IActionResult Layoutdark()
        {
            ViewBag.Bodyclass = "dark-only";
            return View();
        }

        public IActionResult Boxed()
        {
            ViewBag.Bodyclass = "box-layout";
            return View();
        }

        public IActionResult Layoutrtl()
        {
            ViewBag.Bodyclass = "rtl";
            return View();
        }

        public IActionResult Footerlight()
        {
            //ViewBag.Bodyclass = "rtl";
            return View();
        }

        public IActionResult Footerdark()
        {
            //ViewBag.Bodyclass = "rtl";
            return View();
        }

        public IActionResult Footerfixed()
        {
            //ViewBag.Bodyclass = "rtl";
            return View();
        }

    }
}
