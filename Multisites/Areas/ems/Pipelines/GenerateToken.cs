using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.PasswordRecovery;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Pipelines
{
    public class GenerateToken : PasswordRecoveryProcessor
    {
        public override void Process(PasswordRecoveryArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            var user = User.FromName(args.Username, true);
            if (user == null)
            {
                args.AbortPipeline();
                return;
            }
            var token = ID.NewID.ToShortID().ToString();
            StoreTokenOnUser(user, token);
            args.CustomData.Add(Constants.ConfirmTokenKey, token);
        }

        private void StoreTokenOnUser(User user, string confirmToken)
        {
            user.Profile.SetCustomProperty(Constants.ConfirmTokenKey, confirmToken);
            user.Profile.Save();
        }
    }
    internal struct Constants
    {
        internal const string ConfirmTokenKey = "PasswordToken";
    }
}