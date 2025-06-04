using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.Web.Http;
using NM_MultiSites.Areas.Innovation.Mappers.Forms;

namespace NM_MultiSites.Areas.Innovation
{
    public class CaptchaController : ApiController
    {
        [HttpGet]
        public string CaptchaResponseTest()
        {
          
            return "Success" ;
        }
        [HttpPost]
        public bool ValidCaptchaResponse()
        {
            var secrets = ContactUsFormService.GetSecureSecretSettings();
            string captchaResponse = Request.Content.ReadAsStringAsync().Result;
            if (captchaResponse != null && captchaResponse.Length > 0)
            {
                using (var client = new WebClient())
                {
                    var validationResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secrets.GoogleCaptchaSecretKey, captchaResponse));
                    return JsonConvert.DeserializeObject<RecaptchaResponse>(validationResult).Success;
                }
            }
            return false;
        }

        private class RecaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
        }
    }
}