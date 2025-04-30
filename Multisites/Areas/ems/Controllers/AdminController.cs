using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Web.UI.WebControls;
using NM_MultiSites.Areas.ems.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using NM_MultiSites.Areas.ems.Helper;
using System.Text;
using NM_MultiSites.Areas.ems.Models.User;
using System.Web.Security;
using Sitecore.Security.Accounts;
using System.IO;
using System.Data;

namespace NM_MultiSites.Areas.ems.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult CreateUser()
        {
            NMPNUser user = new NMPNUser();
            return View(user);
        }

        [HttpPost]
        public ActionResult CreateUser(NMPNUser user)
        {
            var formitem = Sitecore.Context.Database.GetItem("/sitecore/content/NMPN/settings/Form");
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            try
            {
                bool userAdded = UserAction(user, Action.Add.ToString());
                if (userAdded)
                {
                    string subject = String.Format("User Details for {0}", user.Name);
                    string toaddress = user.Email;
                    string htmlString = String.Format(@"<html>

                      <body>
                      <p>Hi {0},</p>
                      <p>Please find below your Username and password for NMPN site.</p>
                      <p>UserName : {1} <br>Password : {2}</br></p>
                      </body>
                      </html>", user.Name, user.UserName, user.Password);

                    Email.SendEMail("no-reply@nm.org", toaddress, subject, htmlString);
                    return View("~/Areas/ems/Views/Forms/Success.cshtml");
                }
                else
                {
                    return View("~/Areas/ems/Views/Forms/Error.cshtml");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "SendMail");
                return View("~/Areas/ems/Views/Forms/Error.cshtml");
            }
        }

        public ActionResult ImportExcel()
        {


            return View();
        }


        [HttpPost]
        public ActionResult ImportExcel(string input = null)
        {


            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                string query = null;
                string connString = "";




                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}\\{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["FileUpload1"].SaveAs(path1);
                    DataTable dt;
                    if (extension == ".csv")
                    {
                        dt = FileUtil.ConvertCSVtoDataTable(path1);
                        ViewBag.Data = ImportUserList(dt);
                    }
                    //Connection String to Excel Workbook  
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        dt = FileUtil.ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        dt = FileUtil.ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }

                    
                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";

                }

            }

            return View();
        }

        private DataTable ImportUserList(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                var ImportSuccesful = new DataColumn("ImportSuccesful", typeof(bool));
                var EmailSent = new DataColumn("EmailSent", typeof(bool));
                dt.Columns.Add(ImportSuccesful);
                dt.Columns.Add(EmailSent);
                foreach (DataRow row in dt.Rows)
                {
                    NMPNUser usr = new NMPNUser()
                    {
                        UserName = row["USER ID"].ToString(),
                        Name = row["FIRST NAME"].ToString() + ", " + row["LAST NAME"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        IsAdmin = row["ADMINISTRATOR"].ToString() == "x" ? true : false,
                        NPIs = row["NPI"].ToString().Replace("&",","),
                        Password = Membership.GeneratePassword(10, 3)
                };

                    try
                    {
                        bool userAdded = UserAction(usr, Action.Add.ToString());
                        if (userAdded)
                        {
                            row["ImportSuccesful"] = true;
                            string subject = String.Format("User Details for {0}", usr.Name);
                            string toaddress = usr.Email;
                            string htmlString = String.Format(@"<html>

                                  <body>
                                  <p>Hi {0},</p>
                                  <p>Please find below your Username and password for NMPN site.</p>
                                  <p>UserName : {1} <br>Password : {2}</br></p>
                                  </body>
                                  </html>", usr.Name, usr.UserName, usr.Password);
                            try
                            {
                                Email.SendEMail("no-reply@nm.org", toaddress, subject, htmlString);
                                row["EmailSent"] = true;
                            }
                            catch(Exception ex)
                            {
                                row["EmailSent"] = false;
                            }
                            
                        }
                        else
                        {
                            row["ImportSuccesful"] = false;
                            row["EmailSent"] = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        row["ImportSuccesful"] = false;
                        row["EmailSent"] = false;
                    }
                }
            }
            return dt;
        }

        private bool UserAction(NMPNUser nmpnuser, string action)
        {
            var userName = string.Format("nmpn\\{0}", nmpnuser.UserName);
            try
            {
                if (action == Action.Add.ToString())
                {
                    if (!LoginHelper.IsValidUser(userName))
                    {
                        Membership.CreateUser(userName, nmpnuser.Password, nmpnuser.Email);
                        User newuser = LoginHelper.GetUser(userName);
                        Sitecore.Security.UserProfile userProfile = newuser.Profile;
                        userProfile.FullName = string.Format("{0}", nmpnuser.Name);
                        userProfile.Comment = "";

                        // Assigning the user profile template
                        userProfile.SetPropertyValue("ProfileItemId", "{720772D8-AC4D-4C6C-981B-3DF80234B0B7}");
                        userProfile.SetCustomProperty("IsNMPNAdmin", nmpnuser.IsAdmin ? "1" : "0");
                        userProfile.SetCustomProperty("NPI Ids", nmpnuser.IsAdmin ? "ADM" : nmpnuser.NPIs);
                        userProfile.Save();
                        Role nmpnuserRole = Role.FromName("NMPN\\NMPNUser");
                        newuser.Roles.Add(nmpnuserRole);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Membership.DeleteUser(userName);
                }
            }
            catch (MembershipCreateUserException ex)
            {
                string log = string.Format("user: {0} : {1}<br>\n", userName, ex.Message);
                return false;
            }

            return true;
        }

        public enum Action
        {
            Add,
            Delete,
            Update
        }

        public class importlist
        {
            public string username { get; set; }
            public bool ImportSuccesful { get; set; }
            public bool EmailSent { get; set; }
        }

    }
}