using MyEvernote.BusinessLayer;
using MyEvernote.WebApp.ViewModels;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //TempData 

            //if (TempData["modelCat"] != null)
            //{
            //    return View(TempData["modelCat"] as List<Note>);
            //}

            NotesManager NoteMan = new NotesManager();
            return View(NoteMan.GetAllNotes().OrderByDescending(x => x.ModifiedOn).ToList());
            // return View(NoteMan.GetAllNotesQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }

            //TempData["modelCat"] = cat.Notes;
            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {
            NotesManager noteMan = new NotesManager();

            return View("Index", noteMan.GetAllNotes().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            //Giriş Kontolü ve yönlendirme
            //Sessionda kullanıcı bilgi saklama
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //Kullanıcı username , eposta kontolü
            //Kayıt işlemi
            //Akticasyon e-postası
            return View();
        }

        public ActionResult UserActivate(Guid activateId)
        {
            //kullanıcı aktivasyonu
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}