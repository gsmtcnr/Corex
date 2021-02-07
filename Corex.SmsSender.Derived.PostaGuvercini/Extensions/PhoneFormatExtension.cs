namespace Corex.SmsSender.Derived.PostaGuvercini
{
    public static class PhoneFormatExtension
    {
        public static string ToPhoneFormat(this string value)
        {
            value = new System.Text.RegularExpressions.Regex(@"\D").Replace(value, string.Empty);
            value = value.TrimStart('1');
            return value;
        }
    }
}
