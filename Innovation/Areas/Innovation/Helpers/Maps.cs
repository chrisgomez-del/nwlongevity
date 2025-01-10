using Sitecore.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Innovation.Areas.Innovation.Helpers
{
    public static class Maps
    {
        public static string GetSignedUrl(string url)
        {
            string keyString = ConfigurationManager.AppSettings["Google:PrivateKey"];

            if (string.IsNullOrEmpty(keyString))
            {
                return url;
            }

            ASCIIEncoding encoding = new ASCIIEncoding();

            // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
            string usablePrivateKey = keyString.Replace("-", "+").Replace("_", "/");
            byte[] privateKeyBytes = Convert.FromBase64String(usablePrivateKey);

            Uri uri = new Uri(url);
            byte[] encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

            // compute the hash
            HMACSHA1 algorithm = new HMACSHA1(privateKeyBytes);
            byte[] hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

            // convert the bytes to string and make url-safe by replacing '+' and '/' characters
            string signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
        }

        public static string GetMapBoxUrl(string lng, string lat, int zoom, int width, int height, int scale, bool marker)
        {
            string keyString = Settings.GetSetting("MapBox:MapKey");
            if (string.IsNullOrEmpty(keyString))
            {
                return "";
            }
            var scaleParam = (scale == 2 ? "@2x" : "");
            var markerString = "pin-s+63599e(" + lng + "," + lat + ")/";
            if (!marker)
            {
                markerString = "";
            }
            var url = string.Format("https://api.mapbox.com/styles/v1/northwesternmedicine/cj13jha7t000b2rrvj57xlpzp/static/{0}{1},{2},{3},0.00,0.00/{4}x{5}{6}?access_token={7}", markerString, lng, lat, zoom, width, height, scaleParam, keyString);
            ASCIIEncoding encoding = new ASCIIEncoding();

            Uri uri = new Uri(url);
            byte[] encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

            // compute the hash
            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query;
        }
    }
}