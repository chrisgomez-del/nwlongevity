using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Forms
{
    public class SecureSecretSettings
    {
        public HtmlString GoogleCaptchaSiteKey { get; set; }

        public HtmlString GoogleCaptchaSecretKey { get; set; }

    }
}