using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Generad.Controllers
{
    public class GeneraDController : Controller
    {
        // GET: GeneraD
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Holding() 
        {
            return View();
        }

        public ActionResult Nosotros() 
        {
            return View();
        }

        public ActionResult Comercializacion() 
        {
            return View();
        }
        public ActionResult IntegracionPersonal()
        {
            return View();
        }

        public ActionResult Documental()
        {
            return View();
        }
        public ActionResult Contacto()
        {
            return View();
        }
        public ActionResult ChangeLanguage(string lang)
        {
            Session["lang"] = lang;
            //string url = Request.UrlReferrer.AbsolutePath.ToString();
            //string getpage = Request.RequestContext.RouteData.Values["view"].ToString();
            //return RedirectToAction(getpage, new { language = lang });
            return RedirectToAction("Index", "GeneraD", new { language = lang });
        }
    }
}