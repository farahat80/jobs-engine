using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
namespace JobBoard.Engines.Helpers
{
    public class Url
    {
        public static string BuildURL(NameValueCollection searchParams,string _apiLink)
        {
            StringBuilder url = new StringBuilder();
            var array = (from key in searchParams.AllKeys
                         from value in searchParams.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
        .ToArray();
            string paramStr = "?" + string.Join("&", array);
            url.Append(_apiLink);
            url.Append(paramStr);
            return url.ToString();

        }
    }
}
