using Sitecore.Data;
using Sitecore.Pipelines.PasswordRecovery;
using Sitecore.Security.Accounts;

namespace NM_MultiSites.Areas.ems.Pipelines
{
    public class GenerateToken
    {
        public void Process(PasswordRecoveryArgs args)
        {
            var user = User.FromName(args.Username, true);
            if (user == null) return;

            // Fix: Replace 'ShortID.NewID' with 'ShortID.NewId()' as per the provided type signature.
            var token = ShortID.NewId().ToString();
            user.Profile.SetCustomProperty("ResetToken", token);
            user.Profile.Save();

            args.CustomData["ResetToken"] = token;
        }
    }
}
