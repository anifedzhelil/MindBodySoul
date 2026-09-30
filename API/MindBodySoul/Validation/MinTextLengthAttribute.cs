using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;

namespace MindBodySoul.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinTextLengthAttribute : ValidationAttribute
    {
        private readonly int minLength;

        public MinTextLengthAttribute(int minLength)
        {
            this.minLength = minLength;
            ErrorMessage = "The {0} field must contain at least {1} characters of text.";
        }

        public override bool IsValid(object? value)
        {
            if (value is not string html) return false;

            var text = Regex.Replace(html, "<.*?>", string.Empty);
            text = WebUtility.HtmlDecode(text).Trim();

            return text.Length >= minLength;
        }

        public override string FormatErrorMessage(string name)
            => string.Format(ErrorMessageString, name, minLength);        
    }
}
