using CodeWithMe.Data;
using CodeWithMe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeWithMe.Controllers
{
    [Area("UI")]
    public class HomeController : Controller
    {

       


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
