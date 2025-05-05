using Sitecore;
using Sitecore.Pipelines.PasswordRecovery;
using Sitecore.Web;

namespace NM_MultiSites.Areas.ems.Pipelines
{
    public class PopulateConfirmMail
    {
        public void Process(PasswordRecoveryArgs args)
        {
            var token = args.CustomData["ResetToken"] as string;
            if (string.IsNullOrEmpty(token))
                return;
            var serverUrl = StringUtil.EnsurePostfix('/', WebUtil.GetServerUrl());
            var resetLink = $"{serverUrl}resetpassword?token={token}&user={args.Username}";

            args.CustomData["EmailSubject"] = "Password Reset Request";
        }
    }
}