using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Forms
{
    public class FormMessages
    {
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SuccessMessage { get; set; }
        public string FailureMessage { get; set; }
        public string EmailSendingError { get; set; }

    }
}