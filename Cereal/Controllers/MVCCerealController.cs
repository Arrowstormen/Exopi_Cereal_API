using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Cereal.Controllers
{
    public class MVCCerealController : Controller
    {
        //
        //  GET: /MVCCereal/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
