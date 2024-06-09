using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

using MVCCRUD.Models;

namespace MVCCRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(Session["EmailId"]==null)
            {
                return RedirectToAction("Index","Profile");
            }
            return View();
        }

        [HttpGet]
        public ActionResult UpdateUser()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcConString"].ToString());
                    con.Open();
                    using (con)
                    {
                        string query = "SELECT [FirstName],[LastName],[Mobile] ,[EmailId],[Address] FROM [dbo].[Account] WHERE EmailId=@EmailId;";
                        SqlCommand cmdFetch = new SqlCommand(query, con);
                        cmdFetch.Parameters.AddWithValue("@EmailId", Session["EmailId"].ToString());
                        SqlDataAdapter sda = new SqlDataAdapter(cmdFetch);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        UpdateAccount user = new UpdateAccount();

                        if (dt.Rows.Count > 0)
                        {
                            
                            user.FirstName = dt.Rows[0]["FirstName"].ToString();
                            user.LastName = dt.Rows[0]["LastName"].ToString();
                            user.MobileNo = dt.Rows[0]["Mobile"].ToString();
                            user.EmailId = dt.Rows[0]["EmailId"].ToString();
                            user.Address = dt.Rows[0]["Address"].ToString();

                            return View(user);
                        }
                        else
                        {
                            ViewBag.Error = "Something went wrong. Please login again.";
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

        [HttpPost]
        public ActionResult UpdateUser(UpdateAccount model)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcConString"].ToString());
                    con.Open();
                    using (con)
                    {
                        string query = "UPDATE [dbo].[Account] SET [FirstName] = @FirstName, [LastName] = @LastName, [Mobile] = @Mobile, [EmailId] = @EmailId, [Address] = @Address WHERE [EmailId] = @OldEmailId;";
                        SqlCommand cmdUpdate = new SqlCommand(query, con);
                        cmdUpdate.Parameters.AddWithValue("@FirstName", model.FirstName);
                        cmdUpdate.Parameters.AddWithValue("@LastName", model.LastName);
                        cmdUpdate.Parameters.AddWithValue("@Mobile", model.MobileNo);
                        cmdUpdate.Parameters.AddWithValue("@EmailId", model.EmailId);
                        cmdUpdate.Parameters.AddWithValue("@Address", model.Address);
                        cmdUpdate.Parameters.AddWithValue("@OldEmailId", Session["EmailId"].ToString());

                        int i = cmdUpdate.ExecuteNonQuery();

                        if (i > 0)
                        {
                            Session["EmailId"] = model.EmailId;
                            ModelState.Clear();
                            ViewBag.Message = "Profile Updated Succesfully..";
                        }
                        else
                        {
                            ViewBag.Error = "Something Went Wrong..";
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

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                try
                {
                    if (model.NewPassword.Equals(model.ConfirmNewPassword))
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcConString"].ToString());
                        con.Open();
                        using (con)
                        {
                            string query = "UPDATE [dbo].[Account] SET [Password] = @Password WHERE [EmailId] = EmailId AND Password=@OldPassword;";
                            SqlCommand cmdUpdate = new SqlCommand(query, con);
                            cmdUpdate.Parameters.AddWithValue("@Password", Helper.Md5(model.NewPassword));
                            cmdUpdate.Parameters.AddWithValue("@OldPassword", Helper.Md5( model.Password));
                            cmdUpdate.Parameters.AddWithValue("@EmailId", Session["EmailId"].ToString());

                            int i = cmdUpdate.ExecuteNonQuery();

                            if (i > 0)
                            {
                                ModelState.Clear();
                                ViewBag.Message = "Password Changed Succesfully..";
                            }
                            else
                            {
                                ViewBag.Error = "Enter Old Password is not correct..";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Error = "New Password and New Confirm Password is not matching.";
                    }
                    
                }
                catch (Exception ex)
                {
                    Session.RemoveAll();
                    ViewBag.Error = "Something went wrong. " + ex.ToString();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Index", "Profile");
        }
    }
}