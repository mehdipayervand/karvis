﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace Karvis.Core
{
    public class KarvisCrawler : IKarvisCrawler
    {
        const string MatchEmailPattern = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";
        const string EmailPattern1 = @"([a-z]+\.)*[a-z]+[@\s]+[a-z\._-]*"; //gmail.com@ Pos-htiban.Ma_rlik    kmu.ac.ir@ ddd   gmail.com @recarmandar  info@protopsystem
        const string EmailPattern2 = @"[a-z\._-]+@([a-z-]+[.,\s]+)[\s.,]+[(net)(ir)(com)(org)]+"; //T , , , jobs@pendarpajouh , com .
        const string EmailPattern3 = @"(com|ir|net|org)[.\s,]+[a-z.-_]+@([a-z.-]+)+"; //  +"فكس: 88256520 com. resume@rayansazeh ",

        /// <summary>
        /// retrieving web content of the given url
        /// </summary>
        public Stream GetWebTextStream(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.UserAgent = "Karvis Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            return stream;
        }

        /// <summary>
        /// retrieving web content of the given url in string format. This solves unicode problems.
        /// </summary>
        public string GetWebText(string url)
        {
            StreamReader reader = new StreamReader(GetWebTextStream(url));
            string htmlText = reader.ReadToEnd();

            return htmlText;
        }

        /// <summary>
        /// Extracts emails from the given text using regular expressions
        /// </summary>
        public string ExtractEmailsByText(ref string content)
        {
            //content must be lowered

            //sometimes we must detect none standard emails too, emails like 'com. resume@rayansazeh'
            //we detect none standard emails after standards. each detected email would be removed from content

            string retval = string.Empty;

            ExtractEmail(ref content, ref retval, MatchEmailPattern);
            ExtractEmail(ref content, ref retval, EmailPattern1);
            ExtractEmail(ref content, ref retval, EmailPattern2);
            ExtractEmail(ref content, ref retval, EmailPattern3);

            //removing last comma
            if (!string.IsNullOrEmpty(retval))
                retval = retval.Remove(retval.Length - 1, 1);

            return retval;
        }

        private static void ExtractEmail(ref string content, ref string retval, string pattern)
        {
            //if atsign does not exists don't bother for searching emails
            if (!content.Contains("@"))
                return;

            MatchCollection matches = Regex.Matches(content, pattern,
                RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);

            foreach (Match match in matches)
            {
                string value = match.Value;

                //detect potential errors
                if (!value.Contains("@"))
                    continue;

                string validEmail = CorrectEmail(value, pattern);
                retval += string.Format("{0},", validEmail);
                content = content.Replace(value, " ");
            }
        }

        /// <summary>
        /// correct email based on detection pattern
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static string CorrectEmail(string value, string pattern)
        {
            switch (pattern)
            {
                case MatchEmailPattern:
                    return value;
                case EmailPattern1:

                    break;
                default:
                    return "N/A";
            }
        }
    }
}
