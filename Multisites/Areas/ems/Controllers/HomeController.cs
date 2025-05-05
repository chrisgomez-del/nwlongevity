using NM_MultiSites.Areas.ems.Helper;
using NM_MultiSites.Areas.ems.Models.User;
using Sitecore.Pipelines;
using Sitecore.Pipelines.PasswordRecovery;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using Sitecore.Web.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.ems.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LoginBar()
        {
            LoginViewModel loginDetails = new LoginViewModel();
            CurrentUserInfo currentUser = UserInfo();
            if (currentUser != null)
            {
                loginDetails.UserName = currentUser.UserName;

            }
            else
            {
                loginDetails.UserName = String.Empty;
            }
            return View(loginDetails);
        }
        [HttpPost]
        public ActionResult LoginBar(LoginViewModel lg)
        {
            if (lg != null && !String.IsNullOrWhiteSpace(lg.Logout) && lg.Logout.ToLower().Contains("logout"))
            {
                AuthenticationManager.Logout();
                if (Request.Cookies[TicketManager.CookieName] != null)
                {
                    Response.Cookies[TicketManager.CookieName].Expires = DateTime.Now.AddDays(-1);
                }
                TryUpdateModel(lg);
                ModelState.Clear();
                Response.Redirect("/", false);
            }
            else
            {
                CurrentUserInfo currentUser = UserInfo();
                if (currentUser != null)
                {
                    lg.UserName = currentUser.UserName;

                }
                else
                {
                    lg.UserName = String.Empty;
                }
            }
            return View(lg);
        }


        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(PasswordUpdate lg)
        {
            if (lg == null)
            {
                Sitecore.Diagnostics.Log.Warn("ChangePassword: PasswordUpdate object is null.", this);
                return View("Error");
            }

            var currentUser = UserInfo();
            if (currentUser == null)
            {
                lg.Message = "Error: Unable to retrieve user information.";
                return View(lg);
            }

            lg.UserName = currentUser.UserName;
            lg.Success = false;

            if (string.Equals(lg.NewPassword, lg.ConfirmPassword, StringComparison.Ordinal) && LoginHelper.ValidateUserPassword(lg.UserName, lg.OldPassword))
            {
                lg.Success = LoginHelper.ChangePassword(lg.UserName, lg.NewPassword);
            }

            lg.Message = lg.Success ? "Success: Password has been changed." : "Error: Failed to update password.";
            return View(lg);
        }

        public ActionResult ResetPassword()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(PasswordUpdate lg)
        {
            var passwordRecoveryArgs = new PasswordRecoveryArgs(System.Web.HttpContext.Current)
            {

                Username = "nmpn\\"+lg.UserName
            };
            Pipeline.Start("passwordRecovery", passwordRecoveryArgs);
            TryUpdateModel(lg);
            ModelState.Clear();
            lg.UserName = "";
            lg.Message = "Your password has been sent to your Email. If you do not receive an e-mail with your password, please check that you've typed your user name correctly or contact your administrator.";

            return View(lg);
        }

        public ActionResult Login()
        {

            //ViewBag.Message = "Your contact page.";

            return View(UserInfo());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel lg)
        {
            if (lg != null && !String.IsNullOrWhiteSpace(lg.Logout) && lg.Logout.ToLower().Contains("logout"))
            {
                AuthenticationManager.Logout();
                if (Request.Cookies[TicketManager.CookieName] != null)
                {
                    Response.Cookies[TicketManager.CookieName].Expires = DateTime.Now.AddDays(-1);
                }
                return View();
            }
            else if (Currentticketvalid())
            {
                return View(UserInfo());
            }
            var user = LoginHelper.GetUser("nmpn",lg.UserName, lg.Password);
            if(user !=null)
            {
                LoginHelper.Login("nmpn", lg.UserName, lg.Password);
            }
            var IsUserAuthenticated = Sitecore.Context.User.IsAuthenticated;

            // This will return "extranet\user@domain.com"
            var UserName = Sitecore.Context.User.Name;
            string ticket = string.Empty;
            if (!UserName.ToLower().Contains("anonymous"))
            {
                AuthenticationManager.Login(UserName);
                ticket = Sitecore.Web.Authentication.TicketManager.CreateTicket(UserName, @"/sitecore/shell");
                HttpContext current = System.Web.HttpContext.Current;
                if (current != null)
                {
                    int minutes = Convert.ToInt32(Sitecore.Configuration.Settings.GetSetting("UserSessionTimeout"));
                    HttpCookie cookie = new HttpCookie(Sitecore.Web.Authentication.TicketManager.CookieName, ticket)
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddMinutes(minutes)
                    };
                    current.Response.AppendCookie(cookie);
                }
                //Create Session on 
                Session["LoggedinUser"] = UserName;
                Session["Ticket"] = ticket;
                string ReturnUrl = Convert.ToString(Request.QueryString["url"]);
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    Response.Redirect(ReturnUrl, false);
                }
                else
                {
                    Response.Redirect("/dashboard", false);
                }
                
            }
            else
            {
                ViewData["Message"] = "Invalid username or password";
            }
            return View(UserInfo());
        }
        private CurrentUserInfo UserInfo()
        {
            CurrentUserInfo usr = new CurrentUserInfo();
            var logincookie = System.Web.HttpContext.Current.Request.Cookies["sitecore_userticket"];
            if (logincookie != null && !String.IsNullOrEmpty(logincookie.Value))
            {
                //Need to handle
                Ticket currentticket = Sitecore.Web.Authentication.TicketManager.GetTicket(logincookie.Value);
                if (currentticket == null)
                {
                    return null;
                }
                User currentuser = LoginHelper.GetUser(currentticket.UserName);
                usr.UserName = currentuser.Profile.UserName;
                usr.Email = currentuser.Profile.Email;
                usr.NPIs = currentuser.Profile.GetCustomProperty("IsNMPNAdmin") == "1" ? "ADM" : currentuser.Profile.GetCustomProperty("NPI Ids");
            }
            else
            {
                usr = null;
            }
            return usr;

        }
        private bool Currentticketvalid()
        {
            bool case1 = Sitecore.Web.Authentication.TicketManager.IsCurrentTicketValid();
            string ticID = Sitecore.Web.Authentication.TicketManager.GetCurrentTicketId();
            if (!case1 || String.IsNullOrWhiteSpace(ticID))
            {
                return false;
            }

            Ticket currentticket = Sitecore.Web.Authentication.TicketManager.GetTicket(ticID);
            bool case2 = !currentticket.UserName.ToLower().Contains("anonymous");
            return (case1 && case2);

        }
    }
 
}