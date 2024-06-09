using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.SqlClient;
using System.Configuration;

using MVCCRUD.Models;
using System.Data;

namespace MVCCRUD.Controllers
{
    public class ProfileController : Controller
    {
        public static string DbConnectionMVC { get; set; }
        
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(Account model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    DbConnectionMVC = ConfigurationManager.ConnectionStrings["MvcConString"].ToString();
                    SqlConnection con = new SqlConnection(DbConnectionMVC);
                    con.Open();
                    using (con)
                    {
                        if (model.Password.Equals(model.ConfirmPassword))
                        {
                            string query = "INSERT INTO [dbo].[Account]([FirstName],[LastName],[Mobile],[EmailId],[Address],[Password]) VALUES " +
                                "(@FirstName, @LastName, @Mobile, @EmailId, @Address, @Password)";
                            SqlCommand cmdInsert = new SqlCommand(query, con);
                            cmdInsert.Parameters.AddWithValue("@FirstName", model.FirstName);
                            cmdInsert.Parameters.AddWithValue("@LastName", model.LastName);
                            cmdInsert.Parameters.AddWithValue("@Mobile", model.MobileNo);
                            cmdInsert.Parameters.AddWithValue("@EmailId", model.EmailId);
                            cmdInsert.Parameters.AddWithValue("@Address", model.Address);
                            cmdInsert.Parameters.AddWithValue("@Password", Helper.Md5(model.Password));
                            int i = cmdInsert.ExecuteNonQuery();
                            if (i > 0)
                            {
                                ModelState.Clear();
                                ViewBag.Message = "Account Created Succesfully..";
                            }
                            else
                            {
                                ViewBag.Error = "Something Went Wrong..";
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Password and Confirm Password is not matching.";
                        }
                    }
                }
                catch(Exception ex)
                {
                    ViewBag.Error = "Something went wrong. "+ex.ToString();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(LoginAccount model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DbConnectionMVC = ConfigurationManager.ConnectionStrings["MvcConString"].ToString();
                    SqlConnection con = new SqlConnection(DbConnectionMVC);
                    con.Open();
                    using (con)
                    {
                        string query = "SELECT * FROM [dbo].[Account] WHERE EmailId=@EmailId and Password=@Password;";
                        SqlCommand cmdFetch = new SqlCommand(query, con);
                        cmdFetch.Parameters.AddWithValue("@EmailId", model.EmailId);
                        cmdFetch.Parameters.AddWithValue("@Password", Helper.Md5(model.Password));
                        SqlDataAdapter sda = new SqlDataAdapter(cmdFetch);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            Session["EmailId"] = model.EmailId;
                            ModelState.Clear();
                            return RedirectToAction("Index","Home");
                        }
                        else
                        {
                            ViewBag.Error = "Email Id and Password is Wrong..";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Something went wrong. " + ex.ToString();
                }
            }
            return View();
        }
    }
}