using System.Net;
using System.Text.RegularExpressions;

namespace MindBodySoul.Helpers
{
    public static class HtmlTextHelper
    {
        public static string ToPlainText(string html)
        {
            var text = Regex.Replace(html, "<.*?>", string.Empty);
            text = WebUtility.HtmlDecode(text).Trim();

            return text;
        }
    }
}
