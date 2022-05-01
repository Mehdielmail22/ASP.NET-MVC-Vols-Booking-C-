using login.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace login.Controllers
{
    public class AdminController : Controller

    {
        SqlConnection con = new SqlConnection("server=DESKTOP-UOH022O;database=mydb1;integrated security = true");
        mydbEntities2 db = new mydbEntities2();
        // GET: Admin
        

        public ActionResult Login()
        {
            return View();
        }

        //Post:When user click on the submit button then this method will call
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(Loginadmin loginadmin)
        {

            if (IsValid(loginadmin.Email, loginadmin.Password))
            {
                
                FormsAuthentication.SetAuthCookie(loginadmin.Email, false);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Your Email and password is incorrect");
            }
            return View(loginadmin);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
        private bool IsValid(string email, string password)
        {
            
            bool IsValid = false;
            string query = "select * from Gestionnaire where Email=@Email and Password=@Password";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count> 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        public ActionResult Index()
        {
            var vols = from p in db.Vol select p;
            ViewModel3 mymodel = new ViewModel3();
            mymodel.Vols = vols;
            return View(mymodel);
            
        }

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
        public ActionResult Delete(int Id_Vol)
        {
            Vol vol = db.Vol.Find(Id_Vol);
            db.Vol.Remove(vol);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

    }
}