using login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemoProject.Controllers
{
    public class HomeController : Controller
    {

        private mydbEntities2 db = new mydbEntities2();
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Homepage(int? Id_Client)
        {
            Client client = db.Client.Find(Id_Client);
            return View(client);
        }
    }
}