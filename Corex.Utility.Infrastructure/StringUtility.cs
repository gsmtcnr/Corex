using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Corex.Utility.Infrastructure
{
    public static class StringUtility
    {
        public static string RemoveSqlInjectionCharacters(this string input)
        {
            var forbidenchars = new char[] { '\0', '\'', '\"', '\b', '\n', '\r', '\t' };
            var result = string.Empty;
            if (input == null)
                return result;
            foreach (char c in input)
            {
                if (forbidenchars.Contains(c))
                    continue;
                result = $"{result}{c}";
            }
            return result;
        }
        public static string FormatPhoneNumber(this string value)
        {
            value = new System.Text.RegularExpressions.Regex(@"\D")
                .Replace(value, string.Empty);
            value = value.TrimStart('1');
            return value;
        }
        /// <summary>
        /// Belirtilen sayı kadar son haneleri getirir.
        /// </summary>
        /// <param name="count">Son iki hane alınmak isteniyorsa "2" gönderilir.</param>
        public static string GetLast(this string source, int count)
        {
            if (count >= source.Length)
                return source;
            return source.Substring(source.Length - count);
        }
        /// <summary>
        /// Belirtilen sayı kadar sondan karakter siler.
        /// </summary>
        public static string RemoveLast(this string source, int count)
        {
            if (count >= source.Length)
                return source;
            return source.Remove(source.Length - count);
        }
        /// <summary>
        /// Verilen decimal değerin metin karşılığını verir
        /// </summary
        /// <returns></returns>
        public static string ToPriceText(this decimal price)
        {
            string[] strArray = price.ToString().Split('.');
            if (strArray.Length < 2)
                strArray = price.ToString().Split(',');
            string tlSection = GetText(strArray[0]);
            string kurusSection = string.Empty;
            if (strArray.Length > 1)
                kurusSection = GetText(strArray[1]);
            string priceText = "Yalnız ";
            priceText += " " + tlSection != "" ? tlSection + " TL " : "";
            priceText += " " + kurusSection != "" ? kurusSection + " KRŞ" : "";
            return priceText;
        }
        /// <summary>
        /// Fiyatın metin karşılığını veren fonksiyon.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetText(this string value)
        {
            string[] singleDigit = { "", "Bir", "İki", "Üç", "Dört", "Beş", "Altı", "Yedi", "Sekiz", "Dokuz" };
            string[] twiceDigit = { "", "On", "Yirmi", "Otuz", "Kırk", "Elli", "Altmış", "Yetmiş", "Seksen", "Doksan" };
            string[] section = { "", "Yüz", "Bin", "Milyon", "Milyar", "Trilyon" };
            string priceString = "";
            int[] digits = value.Replace(",", "").Select(vi => int.Parse(vi.ToString())).Reverse().ToArray();
            //int[] digits = value.Replace("-", "").Select(vi => decimal.Parse(vi.ToString())).Cast<int>().Reverse().ToArray();

            int rest = digits.Length % 3;
            if (rest == 0)
            {
                for (int i = digits.Length; i > 0; i -= 3)
                {
                    int bolum = i / 3;
                    if (digits[i - 1] != 1 && digits[i - 1] != 0)
                    {
                        priceString += singleDigit[digits[i - 1]] + section[1];
                    }
                    else
                    {
                        if (digits[i - 1] != 0)
                            priceString += section[1];
                    }
                    priceString += twiceDigit[digits[i - 2]];
                    priceString += singleDigit[digits[i - 3]];
                    if (bolum != 1 && (digits[i - 1] != 0 || digits[i - 2] != 0 || digits[i - 3] != 0))
                        priceString += section[bolum] + " ";
                }
            }
            if (rest == 2)
            {
                for (int i = digits.Length + 1; i > 0; i -= 3)
                {
                    int bolum = 0;
                    if (i == digits.Length + 1)
                    {
                        bolum = (digits.Length + 1) / 3;
                        priceString += twiceDigit[digits[i - 2]];
                        priceString += singleDigit[digits[i - 3]];
                        if (bolum != 1 && (digits[i - 2] != 0 || digits[i - 3] != 0))
                            priceString += section[bolum] + " ";
                    }
                    else
                    {
                        bolum = i / 3;
                        if (digits[i - 1] != 1 && digits[i - 1] != 0)
                        {
                            priceString += singleDigit[digits[i - 1]] + section[1];
                        }
                        else
                        {
                            if (digits[i - 1] != 0)
                                priceString += section[1];
                        }
                        priceString += twiceDigit[digits[i - 2]];
                        priceString += singleDigit[digits[i - 3]];
                        if (bolum != 1 && (digits[i - 1] != 0 || digits[i - 2] != 0 || digits[i - 3] != 0))
                            priceString += section[bolum] + " ";
                    }
                }
            }

            if (rest == 1)
            {
                for (int i = digits.Length + 2; i > 0; i -= 3)
                {
                    int bolum = 0;
                    if (i == digits.Length + 2)
                    {
                        bolum = (digits.Length + 2) / 3;
                        if (bolum > 2 || digits[i - 3] != 1)
                            priceString += singleDigit[digits[i - 3]];
                        else if (digits.Length == 1)
                            priceString += singleDigit[digits[i - 3]];
                        if (digits.Length != 1 && digits[i - 3] != 0)
                            priceString += section[bolum] + " ";
                    }
                    else
                    {
                        bolum = i / 3;
                        if (digits[i - 1] != 1 && digits[i - 1] != 0)
                        {
                            priceString += singleDigit[digits[i - 1]] + section[1];
                        }
                        else
                        {
                            if (digits[i - 1] != 0)
                                priceString += section[1];
                        }
                        priceString += twiceDigit[digits[i - 2]];
                        priceString += singleDigit[digits[i - 3]];
                        if (bolum != 1 && (digits[i - 1] != 0 || digits[i - 2] != 0 || digits[i - 3] != 0))
                            priceString += section[bolum] + " ";
                    }
                }
            }
            if (value.Contains('-'))
                priceString = "Eksi " + priceString;

            return priceString;
        }
        public static string ToSafeText(this string incomingText)
        {
            if (string.IsNullOrEmpty(incomingText))
                incomingText = string.Empty;

            incomingText = string.Join("", incomingText.Normalize(NormalizationForm.FormD)
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            incomingText = incomingText.Trim();
            incomingText = incomingText.ToLower();
            incomingText = incomingText.Replace("ş", "s");
            incomingText = incomingText.Replace("Ş", "s");
            incomingText = incomingText.Replace("İ", "i");
            incomingText = incomingText.Replace("I", "i");
            incomingText = incomingText.Replace("ı", "i");
            incomingText = incomingText.Replace("ö", "o");
            incomingText = incomingText.Replace("Ö", "o");
            incomingText = incomingText.Replace("ü", "u");
            incomingText = incomingText.Replace("Ü", "u");
            incomingText = incomingText.Replace("Ç", "c");
            incomingText = incomingText.Replace("ç", "c");
            incomingText = incomingText.Replace("ğ", "g");
            incomingText = incomingText.Replace("Ğ", "g");
            incomingText = incomingText.Replace(" ", "-");
            incomingText = incomingText.Replace("---", "-");
            incomingText = incomingText.Replace("--", "-");
            incomingText = incomingText.Replace("?", "");
            incomingText = incomingText.Replace("/", "");
            incomingText = incomingText.Replace(".", "");
            incomingText = incomingText.Replace("'", "");
            incomingText = incomingText.Replace("#", "");
            incomingText = incomingText.Replace("%", "");
            incomingText = incomingText.Replace("&", "");
            incomingText = incomingText.Replace("*", "");
            incomingText = incomingText.Replace("!", "");
            incomingText = incomingText.Replace(",", "-");
            incomingText = incomingText.Replace("@", "");
            incomingText = incomingText.Replace("+", "");
            incomingText = incomingText.Replace("<b>", "");
            incomingText = incomingText.Replace("</b>", "");
            incomingText = incomingText.Replace(";", "");
            incomingText = incomingText.Replace(":", "");
            incomingText = incomingText.Replace("<br>", "");
            incomingText = incomingText.Replace("<br/>", "");
            incomingText = incomingText.Replace('"'.ToString(), "");
            incomingText = incomingText.Replace("®", "");
            incomingText = incomingText.Replace("’", "");

            incomingText = incomingText.Trim();
            return incomingText;
        }
        
        /// <summary>
        /// Converts given text to Guid
        /// Returns Guid.Empty if not applicable
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Guid.Empty;

            return Guid.Parse(text);
        }
    }
}
