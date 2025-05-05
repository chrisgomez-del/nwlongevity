using NM_MultiSites.Areas.ems.Pipelines;
using Sitecore.Data;
using Sitecore.Pipelines;
using Sitecore.Pipelines.PasswordRecovery;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace NM_MultiSites.sitecore.api.passwordrecovery
{
    public class ConfirmRecoveryController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Confirm(string userName, string token)
        {
            userName = userName.Replace('_', '\\');
            var user = Sitecore.Security.Accounts.User.FromName(userName, true);
            if (user == null || !TokenIsValid(user, token))
                return new StatusCodeResult(HttpStatusCode.Unauthorized, this);

            var passwordRecoveryArgs = new PasswordRecoveryArgs(HttpContext.Current)
            {
                Username = userName
            };
            Pipeline.Start("confirmPasswordRecovery", passwordRecoveryArgs);
            if (!passwordRecoveryArgs.Aborted)
                DeleteToken(user);

            return new StatusCodeResult(HttpStatusCode.OK, this);
        }

        private void DeleteToken(User user)
        {
            user.Profile.SetCustomProperty(Constants.ConfirmTokenKey, string.Empty);
            user.Profile.Save();
        }

        private bool TokenIsValid(User user, string token)
        {
            return !string.IsNullOrEmpty(token) && ShortID.IsShortID(token) && TokenExists(user, token);
        }

        private bool TokenExists(User user, string confirmToken)
        {
            var tokenOnProfile = user.Profile.GetCustomProperty(Constants.ConfirmTokenKey);
            return !string.IsNullOrEmpty(tokenOnProfile) && tokenOnProfile.Equals(confirmToken, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}