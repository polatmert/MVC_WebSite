using MyEvernote.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            TestData testData = new TestData();
            //testData.InsertTest();
            //testData.UpdateTest();
            //testData.DeleteTest();
            testData.CommentTest();
            return View();
        }
    }
}