using CodeWithMe.Data;
using CodeWithMe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CodeWithMe.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationContext _db;

        public AdminController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Languages()
        {
            List<Languages> lang = _db.Languages.ToList();
            List<int> no_of_types = new List<int>();
            List<int> no_of_prog = new List<int>();
            foreach (var i in lang)
            {
                no_of_types.Add(_db.Types.Where(e => e.LangaugeId == i.Id).Count());
            }
            ViewBag.no_of_types = no_of_types;
           
            return View(lang);
        }
        public IActionResult Prog()
        {
            List<Models.Program> l = _db.Program.ToList();
            List<string> Lang = new List<string>();
            List<string> Type = new List<string>();
            foreach(var i in l)
            {
                Languages lan = _db.Languages.Find(i.LanguageId);
                Lang.Add(lan.Name);
                Types typ = _db.Types.Find(i.TypeId);
                Type.Add(typ.Name);
            }
            ViewBag.l = Lang;
            ViewBag.t = Type;


            return View(l);
        }
        public IActionResult Type()
        {
            var l = _db.Types.GroupBy(e => new { e.Name, e.Description }, e => e.Name, (a, b) =>
            new
            {
                Name = a.Name,
                Description = a.Description,

                Count = b.Count()

            }).ToList();
            List<List<object>> t = new List<List<object>>();
            foreach (var i in l)
            {
                t.Add([i.Name, i.Description, i.Count]);
            }
            return View(t);
        }
        public IActionResult LanguagesAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LanguagesAdd(LanguageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Languages attachment = new Languages();

                //Console.WriteLine(model.Description);
                if (model.Attachment != null)
                {
                    //write file to a physical path
                    var uniqueFileName = FileConversion.CreateUniqueFileExtension(model.Attachment.FileName);
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName);
                    model.Attachment.CopyTo(new FileStream(filePath, FileMode.Create));
                    attachment.Attachment = FileConversion.GetByteArrayFromImage(model.Attachment);
                    attachment.File = uniqueFileName;
                }
                //save the attachment to the database
                attachment.Name = model.Name;
                attachment.Description = model.Description;

                _db.Languages.Add(attachment);
                _db.SaveChanges();
                return RedirectToAction("Languages");
            }

            return View();
        }
        [HttpGet("/Admin/Language/Edit/{id}")]
        public IActionResult LangaugeEdit(int id)
        {
            Languages? lang = _db.Languages.Find(id);
            return View("LanguageEdit", lang);
        }
        [HttpGet("/Admin/Language/Delete/{id}")]
        public IActionResult LangaugeDelete(int id)
        {
            Languages l = _db.Languages.Find(id);
            try
            {
                if(!String.IsNullOrEmpty(l.File))
                System.IO.File.Delete("wwwroot/images/" + l.File);

                _db.Languages.Remove(l);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            _db.SaveChanges();
            return Redirect("/Admin/Languages");
        }
        [HttpPost("/Admin/Language/Edit/{id}")]
        public IActionResult LangaugeEdit(int id, LanguageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Languages l = _db.Languages.Find(id);

                //Console.WriteLine(model.Description);
                if (model.Attachment != null)
                {
                    //write file to a physical path
                    var uniqueFileName = FileConversion.CreateUniqueFileExtension(model.Attachment.FileName);
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName);
                    model.Attachment.CopyTo(new FileStream(filePath, FileMode.Create));
                    l.Attachment = FileConversion.GetByteArrayFromImage(model.Attachment);
                    l.File = uniqueFileName;

                }
                //save the attachment to the database
                l.Name = model.Name;
                l.Description = model.Description;


                _db.Languages.Update(l);
                _db.SaveChanges();
                return RedirectToAction("Languages");
            }

            return View();


        }

        public IActionResult ProgAdd()
        {
            List<Languages> l = _db.Languages.ToList();
            List<Types> t=_db.Types.ToList();
            ViewBag.l = l;
            ViewBag.t = t;
            return View();
        }
        [HttpPost]
        public IActionResult ProgAdd(Models.Program model)
        {
            Console.WriteLine(model.TypeId);
            if (ModelState.IsValid)
            {
                Models.Program prog = new Models.Program();
                prog.Name = model.Name;
                prog.Description = model.Description;
                prog.LanguageId = model.LanguageId;
                prog.TypeId = model.TypeId;
                prog.Code = model.Code;
                _db.Program.Add(prog);
                _db.SaveChanges();
                return Redirect("/Admin/Prog");
            }
            List<Languages> l = _db.Languages.ToList();
            List<Types> t = _db.Types.ToList();
            ViewBag.l = l;
            ViewBag.t = t;
            return View();
             
        }
        [HttpGet("/Admin/Prog/Edit/{id}")]
        public IActionResult ProgEdit(int id)
        {
           Models.Program pg = _db.Program.Find(id);
            List<Languages> l = _db.Languages.ToList();
            List<Types> t = _db.Types.ToList();
            ViewBag.l = l;
            ViewBag.t = t;

            return View(pg);

        }

        [HttpPost("/Admin/Prog/Edit/{id}")]
        public IActionResult ProgEdit(int id,Models.Program model)
        {
            if (ModelState.IsValid)
            {
                Models.Program prog = _db.Program.Find(id);
                prog.Name = model.Name;
                prog.Description = model.Description;
                prog.LanguageId = model.LanguageId;
                prog.TypeId = model.TypeId;
                prog.Code = model.Code;
                _db.Program.Update(prog);
                _db.SaveChanges();
                return Redirect("/Admin/Prog");
            }
            List<Languages> l = _db.Languages.ToList();
            List<Types> t = _db.Types.ToList();
            ViewBag.l = l;
            ViewBag.t = t;
            return View();

        }
        [HttpGet("/Admin/Prog/Delete/{id}")]
        public IActionResult ProgDelete(int id)
        {
            try {          
                Models.Program prog = _db.Program.Find(id);
                _db.Program.Remove(prog);
                _db.SaveChanges();
                return Redirect("/Admin/Prog");
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            List<Languages> l = _db.Languages.ToList();
            List<Types> t = _db.Types.ToList();
            ViewBag.l = l;
            ViewBag.t = t;
            return View();

        }
        public IActionResult TypeAdd()
        {
            List<Languages> l = _db.Languages.ToList();

            return View(l);
        }
        [HttpPost]
        public IActionResult TypeAdd(TypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (int i in model.LanguagesId)
                {
                    Models.Types attachment = new Models.Types();


                    //Console.WriteLine(model.Description);
                    if (model.Attachment != null)
                    {
                        //write file to a physical path
                        var uniqueFileName = FileConversion.CreateUniqueFileExtension(model.Attachment.FileName);
                        var filePath = Path.Combine("wwwroot/images", uniqueFileName);
                        model.Attachment.CopyTo(new FileStream(filePath, FileMode.Create));
                        attachment.Attachment = FileConversion.GetByteArrayFromImage(model.Attachment);
                        attachment.File = uniqueFileName;
                    }
                    //save the attachment to the database
                    attachment.Name = model.Name;
                    attachment.Description = model.Description;
                    attachment.LangaugeId = i;
                    _db.Types.Add(attachment);
                    _db.SaveChanges();

                }

                return RedirectToAction("Type");
            }
            List<Languages> l = _db.Languages.ToList();

            return View(l);


        }
        [HttpGet("/Admin/Type/Edit/{name}")]
        public IActionResult TypeEdit(String name)
        {
            List<Types> t = _db.Types.Where(e => e.Name == name).ToList();
            List<int> ids = new List<int>();
            foreach (var i in t)
            {
                ids.Add(i.LangaugeId);
            }
            List<Languages> l = _db.Languages.ToList();
            ViewBag.t = t;
            ViewBag.l = l;
            ViewBag.ids = ids;
            return View();
        }
        [HttpPost("/Admin/Type/Edit/{name}")]
        public IActionResult TypeEdit(string name, TypeViewModel model)
        {
            List<Types> t = _db.Types.Where(e => e.Name == name).ToList();
            List<int> ints = new List<int>();


            if (ModelState.IsValid)
            {
                foreach (var i in t)
                {
                    if (model.LanguagesId.IndexOf(i.LangaugeId) != -1)
                    {
                        
                        //Console.WriteLine(model.Description);
                        if (model.Attachment != null)
                        {
                            //write file to a physical path
                            var uniqueFileName = FileConversion.CreateUniqueFileExtension(model.Attachment.FileName);
                            var filePath = Path.Combine("wwwroot/images", uniqueFileName);
                            model.Attachment.CopyTo(new FileStream(filePath, FileMode.Create));
                            i.Attachment = FileConversion.GetByteArrayFromImage(model.Attachment);
                            i.File = uniqueFileName;
                        }
                        //save the attachment to the database
                        i.Name = model.Name;
                        i.Description = model.Description;
                        i.LangaugeId = i.LangaugeId;
                        _db.Types.Update(i);
                        _db.SaveChanges();
                        model.LanguagesId.Remove(i.LangaugeId);

                    }
                    else
                    {
                        _db.Types.Remove(i);
                        _db.SaveChanges();
                    }
                }
                foreach(int i in model.LanguagesId)
                {
                    Models.Types attachment = new Models.Types();


                    //Console.WriteLine(model.Description);
                    if (model.Attachment != null)
                    {
                        //write file to a physical path
                        var uniqueFileName = FileConversion.CreateUniqueFileExtension(model.Attachment.FileName);
                        var filePath = Path.Combine("wwwroot/images", uniqueFileName);
                        model.Attachment.CopyTo(new FileStream(filePath, FileMode.Create));
                        attachment.Attachment = FileConversion.GetByteArrayFromImage(model.Attachment);
                        attachment.File = uniqueFileName;
                    }
                    //save the attachment to the database
                    attachment.Name = model.Name;
                    attachment.Description = model.Description;
                    attachment.LangaugeId = i;
                    _db.Types.Add(attachment);
                    _db.SaveChanges();

                }


                return RedirectToAction("Type");
            }

            List<Languages> l = _db.Languages.ToList();

            return View(l);

        }
        [HttpGet("/Admin/Type/Delete/{name}")]
        public IActionResult TypeDelete(String name)
        {
            List<Types> t = _db.Types.Where(e => e.Name == name).ToList();
            foreach (var i in t)
            {
                try
                {
                    if (!String.IsNullOrEmpty(i.File))
                        System.IO.File.Delete("wwwroot/images/" + i.File);

                    _db.Types.Remove(i);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                _db.SaveChanges();
            }
            return Redirect("/Admin/Type");
        }
    }
}
