using CodeWithMe.Data;
using CodeWithMe.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace CodeWithMe.Controllers
{
    [Area("UI")]

    public class LanguagesController : Controller
    {
        private readonly ApplicationContext _db;

        public LanguagesController(ApplicationContext db)
        {
            _db=db;
        }

        public IActionResult Index()
        {
            LanguageViewModel model = new LanguageViewModel();
            model.Attachments = _db.Languages.Select(m => m).ToList();
            return View(model);
        }
        [HttpGet("/Languages/{type}/{id}")]
        public IActionResult Type(string type,int id)
        {
           List<Types> types=_db.Types.Where(m => m.LangaugeId==id).ToList();
           ViewBag.Type = type;
            return View(types);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
