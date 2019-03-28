using MyEvernote.BusinessLayer;
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
            return View(NoteMan.GetAllNotes());
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
            return View("Index", cat.Notes);
        }
    }
}