using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using login.Models;
using Microsoft.AspNet.Identity;

namespace login.Controllers
{

    public class VolsController : Controller
    {
        SqlConnection con = new SqlConnection("server=DESKTOP-UOH022O;database=mydb1;integrated security = true");
        private mydbEntities2 db = new mydbEntities2();

        // GET: Vols
   //     [AllowAnonymous]
        [Authorize]

        public ActionResult Index(int Id_client,string q,string l)
        {
            var vols = from p in db.Vol select p;
            if (!string.IsNullOrWhiteSpace(q))
            {
                vols = vols.Where(p => p.Depart.Contains(q));
            }
           else if (!string.IsNullOrWhiteSpace(q)&& !string.IsNullOrWhiteSpace(l))
            {
                vols = vols.Where(p => (p.Depart.Contains(q)&& p.Destination.Contains(l)));
            }
            else if (!string.IsNullOrWhiteSpace(l))
            {
                vols = vols.Where(p => p.Destination.Contains(l));
            }
            ViewModel2 mymodel = new ViewModel2();
            var c = db.Client.Find(Id_client);
            mymodel.Vols = vols;
            mymodel.Clients=c;
            return View(mymodel);
        }
        // GET: Vols/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            return View(vol);
        }

        // GET: Vols/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vols/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Class2 vol)
        {
              string query = "insert into Vol(Nbr_max,Depart,Destination,Prix) values (@Nbr_max,@depart,@Destination,@Prix)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Nbr_max", vol.Nbr_max);
                    cmd.Parameters.AddWithValue("@depart", vol.Depart);
                    cmd.Parameters.AddWithValue("@Destination", vol.Destination);
                    cmd.Parameters.AddWithValue("@Prix", vol.Prix);
                    // encode password i'm using simple base64 you can use any more secure algo
                    //cmd.Parameters.AddWithValue("@Time_depart", vol.Time_depart);
                    //cmd.Parameters.AddWithValue("@Time_arrive", vol.Time_arrive);
                    //cmd.Parameters.AddWithValue("@Date_depart", vol.Date_depart);
                    //cmd.Parameters.AddWithValue("@Date_arrive", vol.Date_arrive);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "something went wrong try later!");
                    }
                }
            

            return View(vol);
        }

        // GET: Vols/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            return View(vol);
        }

        // POST: Vols/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,nbr_places_max,destination,depart,prix")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vol);
        }

        // GET: Vols/Delete/5
        [Authorize]
        //public ActionResult Delete(int? Id_Vol)
        //{
        //    if (Id_Vol == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Vol vol = db.Vol.Find(Id_Vol);
        //    if (vol == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return RedirectToAction("Index", "Admin");
        //}

        // POST: Vols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int Id_Vol)
        {
            Vol vol = db.Vol.Find(Id_Vol);
            db.Vol.Remove(vol);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        [Authorize]

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // public  ActionResult Index(string searchString1,String searchString2)
        // {
        //   var vols = from m in db.Vol
        //                 select m;

        //      if (!String.IsNullOrEmpty(searchString1)&& !String.IsNullOrEmpty(searchString2))
        //      {
        //         vols =vols.Where((s => s.destination.Contains(searchString1)&&s.depart.Contains(searchString2)));
        //      }

        //     return View( vols.ToList());
        //}
        // [HttpPost]
        // public string Index(string searchString1,string searchString2, bool notUsed)
        // {
        //  return "From [HttpPost]Index: filter on " + searchString1+searchString2;
        // }
        //  public ActionResult Index()
        //  {
        //      return View(db.Vol.ToList());
        //  }
        //  [HttpPost]
        //  public JsonResult SearchCustomers(string customerName)
        //  {

        //    var vol = from c in db.Vol
        //               where c.destination.Contains(customerName)
        //               select c;
        //    return Json(vol.ToList().Take(10));
        public ActionResult adminvol( string q, string l)
        {
            var vols = from p in db.Vol select p;
            if (!string.IsNullOrWhiteSpace(q))
            {
                vols = vols.Where(p => p.Depart.Contains(q));
            }
            else if (!string.IsNullOrWhiteSpace(q) && !string.IsNullOrWhiteSpace(l))
            {
                vols = vols.Where(p => (p.Depart.Contains(q) && p.Destination.Contains(l)));
            }
            else if (!string.IsNullOrWhiteSpace(l))
            {
                vols = vols.Where(p => p.Destination.Contains(l));
            }
            ViewModel2 mymodel = new ViewModel2();
            
            mymodel.Vols = vols;
            ;
            return View(mymodel);
        }

    }
}
