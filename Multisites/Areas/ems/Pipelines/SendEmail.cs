using Sitecore.Pipelines.PasswordRecovery;
using Sitecore.Diagnostics;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System;
using System.Web.Security;

namespace NM_MultiSites.Areas.ems.Pipelines
{
    public class SendEmail
    {
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Use Regex to validate email format
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public void Process(PasswordRecoveryArgs args)
        {
            var user = Sitecore.Security.Accounts.User.FromName(args.Username, true);
            if (user == null)
            {
                Sitecore.Diagnostics.Log.Warn($"SendEmail: User not found: {args.Username}", this);
                return;
            }

            var email = user.Profile.Email; // Fetch the email from the user's profile
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                Sitecore.Diagnostics.Log.Warn($"SendEmail: Invalid email address for user: {args.Username}", this);
                return;
            }

            // Regenerate the password using Sitecore Membership API
            var membershipUser = Membership.GetUser(args.Username);
            if (membershipUser == null)
            {
                Sitecore.Diagnostics.Log.Warn($"SendEmail: Membership user not found: {args.Username}", this);
                return;
            }

            // Generate a new password
            var newPassword = membershipUser.ResetPassword();

            var subject = args.CustomData["EmailSubject"] as string ?? "Password Reset Request";
            var body = args.CustomData["EmailBody"] as string ?? "Your password has been reset.";
            body += "\n\nPlease return to the site and log in using the following information:";
            body += $"\nNew Password: {newPassword}";

            var fromAddress = "donotreply@nm.org"; 

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromAddress), 
                Subject = subject,
                Body = body,
                IsBodyHtml = false 
            };
            mailMessage.To.Add(email);

            Sitecore.MainUtil.SendMail(mailMessage);

            Sitecore.Diagnostics.Log.Info($"SendEmail: Password reset email sent to {email}", this);
        }

    }
}