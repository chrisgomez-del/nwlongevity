using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Helpers
{
    public static class Cookie
    {
        private static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public static void Set(string key, string value, int expiresAsDays = 1, bool isHttp = true, string domain = null, string path = null, bool secure = false)
        {
            if (string.IsNullOrEmpty(key))
                return;

            var encodedKey = HttpUtility.UrlEncode(key);

            var encodedValue = !string.IsNullOrEmpty(value)
                ? HttpUtility.UrlEncode(value)
                : string.Empty;

            var c = new HttpCookie(encodedKey)
            {
                Value = encodedValue,
                HttpOnly = isHttp,
                Secure = secure
            };
            if (expiresAsDays > 0)
            {
                c.Expires = DateTime.Now.AddDays(expiresAsDays);
            }

            if (!string.IsNullOrEmpty(domain))
                c.Domain = domain;

            if (!string.IsNullOrEmpty(path))
                c.Path = path;

            Context.Response.Cookies.Add(c);
        }

        public static string Get(string key, bool isHttp = true, string domain = null, string path = null, bool secure = false)
        {
            var returnValue = string.Empty;

            if (string.IsNullOrEmpty(key))
                return returnValue;

            var decodedKey = HttpUtility.UrlDecode(key);
            var c = Context.Request.Cookies[decodedKey];
            if (c == null)
                return returnValue;

            if (isHttp != c.HttpOnly)
                return returnValue;

            if (!string.IsNullOrEmpty(domain) && !string.Equals(c.Domain, domain, StringComparison.Ordinal))
                return returnValue;

            if (!string.IsNullOrEmpty(path) && !string.Equals(c.Path, path, StringComparison.Ordinal))
                return returnValue;

            if (!string.IsNullOrEmpty(c.Value))
                returnValue = HttpUtility.UrlDecode(c.Value).Trim();

            return returnValue;
        }

        public static bool Exists(string key, bool isHttp = true, string domain = null, string path = null, bool secure = false)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            var decodedKey = HttpUtility.UrlDecode(key);
            var c = Context.Request.Cookies[decodedKey];

            if (c == null)
                return false;

            if (isHttp != c.HttpOnly)
                return false;

            if (!string.IsNullOrEmpty(domain) && !string.Equals(c.Domain, domain, StringComparison.Ordinal))
                return false;

            if (!string.IsNullOrEmpty(path) && !string.Equals(c.Path, path, StringComparison.Ordinal))
                return false;

            return true;
        }

        public static void Delete(string key, bool isHttp = true, string domain = null, string path = null, bool secure = false)
        {
            if (!Exists(key, isHttp, domain, path, secure))
                return;

            Set(key, null, -10, isHttp, domain, path, secure);
        }

        public static void DeleteAllCookies(bool isHttp = true, string domain = null, string path = null, bool secure = false, bool deleteServerCookies = false)
        {
            for (var i = 0; i <= Context.Request.Cookies.Count - 1; i++)
            {
                if (Context.Request.Cookies[i] != null)
                    Delete(Context.Request.Cookies[i].Name, isHttp, domain, path, secure);
            }

            if (deleteServerCookies)
                Context.Request.Cookies.Clear();
        }
    }
}