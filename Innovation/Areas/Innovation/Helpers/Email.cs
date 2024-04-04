using Sitecore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace Innovation.Areas.Innovation.Helpers
{
    public class Email
    {
        #region Static Members
        public static void SendEMail(string from, string to, string subject, string body, HttpPostedFileBase attachment = null)
        {
            var emailMessage = new MailMessage(from, to, subject, body);
            if (attachment != null && attachment.ContentLength > 0)
            {
                var filetoattach = new Attachment(attachment.InputStream, attachment.FileName);
                emailMessage.Attachments.Add(filetoattach);
            }
            emailMessage.IsBodyHtml = true;
            MainUtil.SendMail(emailMessage);
        }

        /// <summary>
        /// Determines whether an email address is valid.
        /// </summary>
        /// <param name="emailAddress">The email address to validate.</param>
        /// <returns>
        /// 	<c>true</c> if the email address is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmailAddress(string emailAddress)
        {
            // An empty or null string is not valid
            if (String.IsNullOrEmpty(emailAddress))
            {
                return (false);
            }

            // Regular expression to match valid email address
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            // Match the email address using a regular expression
            Regex re = new Regex(emailRegex);
            if (re.IsMatch(emailAddress))
                return (true);
            else
                return (false);
        }

        #endregion
    }
}