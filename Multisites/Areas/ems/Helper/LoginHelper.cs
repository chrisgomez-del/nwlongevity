using System.Linq;
using System.Security;
using Sitecore.Common;
using Sitecore.Security;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using Sitecore.Web.Authentication;

namespace NM_MultiSites.Areas.ems.Helper
{
    public static class LoginHelper
    {
        public static bool IsValidUser()
        {
            return ((User.Current.IsAuthenticated) && !User.Current.Name.ToLower().Contains("anonymous")) ? true:false;
        }

        public static bool IsValidUser(string userinfo)
        {
            bool isValidUser = false;
            if (User.Exists(userinfo))
            {
                isValidUser = true;
            }
            return isValidUser;
        }


        public static User GetUser(string domainName, string userName, string password)
        {
            if (!System.Web.Security.Membership.ValidateUser(domainName + @"\" + userName, password))
                return null;
            if (User.Exists(domainName + @"\" + userName))
                return User.FromName(domainName + @"\" + userName, true);
            return null;
        }

        public static User GetUser(string userinfo)
        {
            
            if (User.Exists(userinfo))
                return User.FromName(userinfo, true);
            return null;
        }

        public static bool Login(string domainName, string userName, string password)
        {
            return AuthenticationManager.Login(domainName + @"\" + userName, password, false);
        }

        public static bool Login(User user)
        {
            string ticketID = TicketManager.GetCurrentTicketId();
            if (!string.IsNullOrEmpty(ticketID))
                TicketManager.RemoveTicket(ticketID);
            return AuthenticationManager.Login(user);
        }

        public static User GetUserFromCustomField(string fieldName, string fieldValue)
        {
            IFilterable<User> allUsers = UserManager.GetUsers();
            return allUsers.Where(user => user.Profile.GetCustomProperty(fieldName) == fieldValue).FirstOrDefault();
        }

        public static void SetCustomField(User user, string fieldName, string fieldValue)
        {
            UserProfile profile = user.Profile;
            profile.SetCustomProperty(fieldName, fieldValue);
            profile.Save();
        }
        public static bool ValidateUserPassword(string username,string  password)
        {
            //string userString = domain + @"\" + username;
            return System.Web.Security.Membership.ValidateUser(username, password);
        }

        public static bool ChangePassword(string username, string newPassword)
        {
            //var userString = domain + @"\" + username;
            var user = System.Web.Security.Membership.GetUser(username);
            string oldPassword = user.ResetPassword();
            return user.ChangePassword(oldPassword, newPassword);
        }
    }
}