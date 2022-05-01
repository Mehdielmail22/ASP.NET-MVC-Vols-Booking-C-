using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using login.Models;
namespace login.Controllers
{
    public class ReservationController : Controller
    {
        SqlConnection con = new SqlConnection("server=DESKTOP-UOH022O;database=mydb1;integrated security = true");

        private mydbEntities2 db = new mydbEntities2();
        // GET: Reservation
        [HttpGet]
        [Authorize]
        public ActionResult Index(int? Id_Vol, int? Id_Client)
        {
            if (Id_Vol == null || Id_Client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vol = db.Vol.Find(Id_Vol);
            var client = db.Client.Find(Id_Client);
            if (vol == null || client == null)
            {
                return HttpNotFound();
            }
            ViewModel mymodel = new ViewModel(); 
            mymodel.Vols = vol;
            mymodel.Clients = client;
            return View(mymodel);
        }
        public ActionResult Index()
        {
            return RedirectToAction("login", "Account");
        }
        [Authorize]
        public ActionResult Choosepayment(int prix, int? Id_Vol, int? Id_Client)
        {
            if (Id_Vol == null || Id_Client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vol = db.Vol.Find(Id_Vol);
            var client = db.Client.Find(Id_Client);
            if (vol == null || client == null)
            {
                return HttpNotFound();
            }
            res res1 = new res();
            res1.Vols = vol;
            res1.Clients = client;
            res1.Prix = prix;
            return View(res1);
            
        }
        [Authorize]
        public ActionResult Payment(int prix, int? Id_Vol, int? Id_Client)
        {
            if (Id_Vol == null || Id_Client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vol = db.Vol.Find(Id_Vol);
            var client = db.Client.Find(Id_Client);
            if (vol == null || client == null)
            {
                return HttpNotFound();
            }
            res res1 = new res();
            res1.Vols = vol;
            res1.Clients = client;
            res1.Prix = prix;
            return View(res1);

        }
        [Authorize]
        public ActionResult effectuerPayment(int prix, int? Id_Vol, int? Id_Client)
        {
            if (Id_Vol == null || Id_Client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(Id_Vol);
            Client client = db.Client.Find(Id_Client);
            if (vol == null || client == null)
            {
                return HttpNotFound();
            }
            //Reservation res2=new Reservation();
            //res2.Client_Id=client.Id;
            //res2.Vol_Id=vol.Id;
            //res2.Client=client;
            //res2.Vol = vol; 
            //db.Reservations.Add(res2);
            //db.SaveChanges();

            string query = "insert into Reservation(Client_ID,Vol_ID) values (@Client_ID,@Vol_ID)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Vol_ID", vol.Id);
                cmd.Parameters.AddWithValue("@Client_ID", client.Id);
              
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return RedirectToAction("ticket", "Clients", new {Id_client= client.Id });
                }
                else
                {
                    return RedirectToAction("ticket", "Clients", new { Id_client = client.Id });
                }
            }

        }
        public ActionResult Cancel(int id_client,int id_vol) {
            string query = "delete from Reservation where Vol_ID=@Vol_ID and Client_ID=@Client_ID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Vol_ID", id_vol);
                cmd.Parameters.AddWithValue("@Client_ID", id_client);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return RedirectToAction("ticket", "Clients", new { Id_client = id_client });
                }
                else
                {
                    return RedirectToAction("ticket", "Clients", new { Id_client = id_client });
                }
            }

            

        }
    }
}