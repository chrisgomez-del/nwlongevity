using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Sitecore;
using Sitecore.Data;
using Sitecore.Sites;
using Convert = System.Convert;
using SC = Sitecore.Context;
using HtmlAgilityPack;
using Sitecore.StringExtensions;

namespace NM_MultiSites.Areas.Innovation.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        #region Class Variables

        private static readonly string _dash = "-";

        private static readonly string _regSeo = "^[a-z0-9-]*$";

        private static readonly string[] _truthyValues = new string[] { "1", "y", "yes", "t", "true" };

        private static readonly string[] _falseyValues = new string[] { "0", "n", "no", "f", "false" };

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string ToPhrase(this string phrase)
        {
            return "\"" + phrase + "\"";
        }

        #region Methods

        /// <summary>
        /// Appends the line format.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static StringBuilder AppendLineFormat(this StringBuilder stringBuilder, string format, params object[] args)
        {
            return stringBuilder.AppendLine(string.Format(format, args));
        }

        /// <summary>
        /// Cleans the phone number
        /// </summary>
        /// <param name="phone">The phone number.</param>
        /// <returns></returns>
        public static string CleanPhone(this string phone)
        {
            if (phone.IsNullOrEmpty()) return phone;

            var digitsOnly = new Regex(@"[^\d]");

            return digitsOnly.Replace(phone, "");
        }

        /// <summary>
        /// Determines whether this string contains another string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toCheck">To check.</param>
        /// <param name="comp">The comp.</param>
        /// <returns>
        ///   <c>true</c> whether this string contains another string; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Converts the first character of the string to upper case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">FirstCharToUpper: invalid arguments for string manipulation</exception>
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("FirstCharToUpper: invalid arguments for string manipulation");

            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        /// <summary>
        /// Gets the value or returns <c>null</c>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueAsString">The value as string.</param>
        /// <returns></returns>
        public static T? GetValueOrNull<T>(this string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;

            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }

        /// <summary>
        /// Determines whether a string represents a valid absolute URL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the value is a valid absolute URL; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAbsoluteUrl(this string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out Uri result);
        }

        /// <summary>
        /// Determines whether this instance is alias.
        /// </summary>
        /// <param name="localPath">The local path.</param>
        /// <returns>
        ///   <c>true</c> if the specified local path is alias; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlias(this string localPath)
        {
            Database database = SC.Database;

            if (database == null)
            {
                return false;
            }
            var aliasPath = MainUtil.DecodeName('/' + SC.Site.Properties["alias"] + '/' + localPath);

            return database.Aliases.Exists(aliasPath);
        }

        /// <summary>
        /// Determines whether this instance has a value that can be converted to a boolean.
        /// <para>Truthy values: '1', 'y', 'yes', 't', 'true'</para>
        /// <para>Falsey values: '0', 'n', 'no', 'f', 'false'</para>
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> this instance has a value that can be converted to a boolean; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBoolean(this string value)
        {
            bool isBool = false;

            if (value.NotIsNullOrWhitespace())
            {
                string tmpVal = value.Trim().ToLower();

                isBool = _truthyValues.Contains(tmpVal) || _falseyValues.Contains(tmpVal);
            }

            return isBool;
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters. 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the value parameter is null, System.String.Empty, or if value consists exclusively of white-space characters.
        /// </returns>
        public static bool IsNullOrWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Determines whether a string represents a valid relative URL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the value is a valid relative URL; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRelativeUrl(this string value)
        {
            return Uri.TryCreate(value, UriKind.Relative, out Uri result);
        }

        /// <summary>
        /// Determines whether [is seo friendly].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is seo friendly] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSeoFriendly(this string value)
        {
            return Regex.IsMatch(value, _regSeo);
        }

        /// <summary>
        /// Converts a string to a list based on a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<string> LinesToList(this string value)
        {
            return value.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        /// <summary>
        /// Indicates whether a specified string <c>NOT</c> a null or an empty string.
        /// </summary>
        /// <param name="myString">My string.</param>
        /// <returns></returns>
        public static bool NotIsNullOrEmpty(this string myString)
        {
            return !myString.IsNullOrEmpty();
        }

        /// <summary>
        /// Indicates whether a string is <c>NOT</c> a null, empty or whitespace string.
        /// </summary>
        /// <param name="myString">My string.</param>
        /// <returns></returns>
        public static bool NotIsNullOrWhitespace(this string myString)
        {
            return !myString.IsNullOrWhitespace();
        }

        /// <summary>
        /// Parses the youTube identifier.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string ParseYouTubeId(this string url)
        {
            if (url == null) return "";

            var youtubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
            var youtubeMatch = youtubeVideoRegex.Match(url);

            var id = string.Empty;

            if (youtubeMatch.Success)
            {
                id = youtubeMatch.Groups[1].Value;
            }

            return id;
        }

       
        /// <summary>
        /// Removes all whitespace.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string RemoveAllWhitespace(this string value)
        {
            return value?.Trim().Replace(" ", string.Empty);
        }

        /// <summary>
        /// Safes the convert.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T SafeConvert<T>(this string value)
        {
            try
            {
                var result = (T)Convert.ChangeType(value, typeof(T));
                return result;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Sanitizes the punctuation.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string SanitizePunctuation(this string input)
        {
            var spaceCharPattern = new Regex(@"[&\-\/\\]");
            var multipleSpacePattern = new Regex(@"[ ]{2,}");
            var cleanString = spaceCharPattern.Replace(input, " ");
            cleanString = multipleSpacePattern.Replace(cleanString, " ");

            cleanString = new string(cleanString.Where(c => !char.IsPunctuation(c)).ToArray());

            return cleanString;
        }

        /// <summary>
        /// Removes anything not a letter, number or whitespace and then change spaces to dashes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Slugify(this string value)
        {
            var normalized = Regex.Replace(value.ToLower(), @"[^a-zA-Z_0-9+\s]", "", RegexOptions.ECMAScript);
            var despaced = Regex.Replace(normalized, @"\s+", " ", RegexOptions.ECMAScript).Trim();
            var hyphenate = despaced.Replace(" ", "-");

            return hyphenate;
        }

        /// <summary>
        /// Strips the HTML.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="decodeHtml">if set to <c>true</c> will also decode the html string.</param>
        /// <returns></returns>
        public static string StripHtml(this string input, bool decodeHtml = false)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(input);

                // Strip away any style and script sections
                doc.DocumentNode.SelectNodes("//style|//script")?.ToList().ForEach(n => n.Remove());
                input = doc.DocumentNode.InnerHtml;
            }
            catch (Exception)
            {
                // Do nothing
            }

            // Post-processing (remove tags and multiple spaces)
            input = Regex.Replace(input, @"<[^>]+>|&nbsp;", " ").Trim();
            input = Regex.Replace(input, @"\s{2,}", " ");

            if (decodeHtml) { input = System.Web.HttpUtility.HtmlDecode(input); }

            return input;
        }

        /// <summary>
        /// Converts a delimited string to an array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="splitChar">The split character.</param>
        /// <returns></returns>
        public static string[] ToArray(this string value, char splitChar = ',')
        {
            if (value.NotIsNullOrWhitespace())
            {
                return value.Split(new char[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
            }

            return new string[] { };
        }

        /// <summary>
        /// Converts a string to an integer array if the values can be converted to integers.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="splitChar">The split character.</param>
        /// <returns></returns>
        public static int[] ToIntArray(this string value, char splitChar = ',')
        {
            if (value.NotIsNullOrWhitespace())
            {
                return value.Split(new char[] { splitChar }, StringSplitOptions.RemoveEmptyEntries).Where(x => int.TryParse(x, out int result)).Select(x => int.Parse(x)).ToArray();
            }

            return new int[] { };
        }

        /// <summary>
        /// Converts the value to a boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value to return if the value cannot be converted.</param>
        /// <returns></returns>
        public static bool ToBoolean(this string value, bool defaultValue = false)
        {
            bool isBool = defaultValue;

            if (value.IsBoolean())
            {
                isBool = _truthyValues.Contains(value.Trim().ToLower());
            }

            return isBool;
        }

        /// <summary>
        /// Converts the string to something SEO-friendly.
        /// </summary>
        /// <param name="value">The title.</param>
        /// <param name="includeExistingHyphens">if set to <c>true</c> include existing hyphens in conversion.</param>
        /// <returns></returns>
        public static string ToSeoFriendly(this string value, bool includeExistingHyphens = true)
        {
            string seoValue = string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                seoValue = "";
            }
            else if (value.IsSeoFriendly())
            {
                seoValue = value.ToLower();
            }
            else
            {
                int maxLength = 2000;
                bool maxLengthHit = false;
                StringBuilder result = new StringBuilder("");
                string pattern = includeExistingHyphens ? "[\\w-]+" : "[\\w]+";
                Match curMatch = Regex.Match(value.ToLower(), pattern);
                bool hadEndingHyphen = includeExistingHyphens ? false : value.EndsWith("-");

                // Process
                while (curMatch.Success && !maxLengthHit)
                {
                    if (result.Length + curMatch.Value.Length <= maxLength)
                    {
                        if (includeExistingHyphens)
                        {
                            if (curMatch.Value.EndsWith(_dash))
                            {
                                result.Append(curMatch.Value);
                            }
                            else
                            {
                                result.Append(curMatch.Value + _dash);
                            }
                        }
                        else
                        {
                            result.Append(curMatch.Value + _dash);
                        }
                    }
                    else
                    {
                        maxLengthHit = true;

                        // Handle a situation where there is only one word and it is greater than the max length.
                        if (result.Length == 0) result.Append(curMatch.Value.Substring(0, maxLength));
                    }
                    curMatch = curMatch.NextMatch();
                }

                // Remove trailing '-'
                if (!includeExistingHyphens || (includeExistingHyphens && !hadEndingHyphen))
                {
                    if (result.Length > 0 && result[result.Length - 1] == '-') result.Remove(result.Length - 1, 1);
                }

                // Final value
                seoValue = result.ToString();
            }

            return seoValue;
        }

        /// <summary>
        /// Converts to a Sitecore <see cref="ID"/>.
        /// </summary>
        /// <param name="sitecoreId">The sitecore identifier.</param>
        /// <returns></returns>
        public static ID ToSitecoreId(this string sitecoreId)
        {
            if (sitecoreId == null) return null;

            return new ID(sitecoreId);
        }

        /// <summary>
        /// Truncates the specified maximum chars.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxChars">The maximum chars.</param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

        /// <summary>
        /// Truncates the specified maximum chars.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxChars">The maximum chars.</param>
        /// <param name="ellipsis">if set to <c>true</c> [ellipsis].</param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxChars, bool ellipsis)
        {
            string trimmedValue = value.Trim();

            if (trimmedValue.Length > maxChars)
            {
                trimmedValue = trimmedValue.Substring(0, maxChars);
                int lastSpaceIndex = trimmedValue.LastIndexOf(" ");

                trimmedValue = trimmedValue.Substring(0, (lastSpaceIndex)) + (ellipsis ? "..." : "");
            }
            return trimmedValue;
        }

        /// <summary>
        /// Un-slugifies the string (see <see cref="Slugify(string)"/>
        /// </summary>
        /// <param name="slug">The slug.</param>
        /// <returns></returns>
        public static string Unslugify(this string slug)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(slug.Replace('-', ' '));
        }

        /// <summary>
        /// Converts the string ID from regular GUID to lower case, no hyphens
        /// e.g., {A1CC2C16-E061-442D-88EE-1A6FE201929A} becomes a1cc2c16e061442d88ee1a6fe201929a
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ToSolrId(this string id)
        {
            return id
                .Replace("-", string.Empty)
                .Replace("{", string.Empty)
                .Replace("}", string.Empty)
                .ToLower();
        }

        #endregion
    }
}