using CodeWithMe.Data;
using CodeWithMe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace CodeWithMe.Controllers
{
    [Area("UI")]
    public class TypeController : Controller
    {
        private readonly ApplicationContext _db;

        public TypeController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet("Type/{lang}/{type}")]
        
        public IActionResult Index(string lang,int type)
        {
            Languages lid = _db.Languages.Where(e => e.Name == lang).FirstOrDefault();
            List<Types> l = _db.Types.Where(e => e.LangaugeId == lid.Id).ToList();
            
            ViewBag.list = l;
            ViewBag.lang = lang;
            List<Models.Program> prog = _db.Program.Where(e => e.TypeId == type && e.LanguageId==lid.Id).ToList();
            return View("Type",prog);
        }
        [HttpGet("Type/Prog/{id}")]
        public IActionResult Prog(int id)
        {
            Models.Program p = _db.Program.Find(id);
           
            Languages l = _db.Languages.Where(e => e.Id == p.LanguageId).First();
            List<Types> t = _db.Types.Where(e => e.LangaugeId == l.Id).ToList();

            ViewBag.lang = l.Name;
            ViewBag.list = t;
            return View(p);
        }

    }
}
