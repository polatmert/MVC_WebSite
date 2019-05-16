using MyEvernote.BusinessLayer;
using MyEvernote.Entities.ValueObject;
using MyEverNote.Entities;
using MyEverNote.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if(ModelState.IsValid)
            {
                EvernoteUserManager everUserManager = new EvernoteUserManager();
                BusinessLayerResult<EverNoteUser> result = everUserManager.LoginUser(model);

                if (result.Errors.Count > 0)
                {
                    if(result.Errors.Find(x=>x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-4567-7890";  // Example User Guid
                    }

                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                Session["login"] = result.Result;  //Session'da bilgi saklama
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                EvernoteUserManager everUserManager = new EvernoteUserManager();
                BusinessLayerResult<EverNoteUser> res = everUserManager.RegisterUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
            }

            return RedirectToAction("RegisterOk");
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivate(Guid id)
        {
            EvernoteUserManager eum = new EvernoteUserManager();
            BusinessLayerResult<EverNoteUser> res = eum.ActivateUser(id);

            if (res.Errors.Count>0)
            {
                TempData["errors"] = res.Errors;
                return RedirectToAction("UserActivateCancel");
            }
            return RedirectToAction("UserActivateOk");
        }

        public ActionResult UserActivateOk()
        {
            return View();
        }


        public ActionResult UserActivateCancel()
        {
            List<ErrorMessageObject> errors = null;

            if (TempData["errors"] != null)
            {
              errors = TempData["errors"] as List<ErrorMessageObject>;
            }
            return View(errors);
        }
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }
    }
}