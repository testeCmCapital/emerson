using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Infrastructure.CrossCutting.Helpers
{
    public static class Utils
    {
        public static bool IsJson(string input)
        {
            try
            {
                JObject.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }





        public static string GetEnumDescription(Enum value)
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
                return attributes.First().Description;

            return value.ToString();
        }

        public static bool ValidateResultIsBoolean(object result)
        {
            var isBool = bool.TryParse(string.Format("{0}", result), out bool val);

            if (isBool)
                return !val;

            return false;
        }

        public static string GetStringIDs(int?[] ids)
        {
            if (ids != null && ids.Length > 0 && !ids.Contains(0))
                return string.Join(',', ids.ToArray());

            return null;
        }

        public static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public static string EncodeToken(string token)
        {
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            return WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
        }

        public static string DecodeToken(string encodedToken)
        {
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(encodedToken);
            return Encoding.UTF8.GetString(codeDecodedBytes);
        }

        public static string GetNumbers(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";

            var numbers = "";

            foreach (char c in text)
                if (char.IsDigit(c))
                    numbers = string.Format("{0}{1}", numbers, c);

            return numbers;
        }

        public static bool IsCnpj(string cnpj)
        {
            cnpj = GetNumbers(cnpj);
            cnpj = cnpj.PadLeft(14, '0');

            if (!long.TryParse(cnpj, out long resultNumber))
                return false;

            if (resultNumber == 0)
                return false;

            int[] mult1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int some = 0;

            for (int i = 0; i < 12; i++)
                some += int.Parse(tempCnpj[i].ToString()) * mult1[i];

            int rest = (some % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            string digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            some = 0;
            for (int i = 0; i < 13; i++)
                some += int.Parse(tempCnpj[i].ToString()) * mult2[i];

            rest = (some % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            return cnpj.EndsWith(digit);
        }

        public static Guid? GetGuidByString(string guidString)
        {
            if (Guid.TryParse(guidString, out var result))
                return result;

            return null;
        }


        public static string GetRelativeDateTime(DateTime date)
        {
            TimeSpan ts = DateTime.Now.AddHours(-3) - date;
            if (ts.TotalMinutes < 1)//seconds ago
                return "agora";
            if (ts.TotalHours < 1)//min ago
                return (int)ts.TotalMinutes == 1 ? "1 minuto" : (int)ts.TotalMinutes + " minutos ";
            if (ts.TotalDays < 1)//hours ago
                return (int)ts.TotalHours == 1 ? "1 hora" : (int)ts.TotalHours + " horas";
            if (ts.TotalDays < 7)//days ago
                return (int)ts.TotalDays == 1 ? "1 dia" : (int)ts.TotalDays + " dias";
            if (ts.TotalDays < 30.4368)//weeks ago
                return (int)(ts.TotalDays / 7) == 1 ? "1 semana" : (int)(ts.TotalDays / 7) + " semanas";
            if (ts.TotalDays < 365.242)//months ago
                return (int)(ts.TotalDays / 30.4368) == 1 ? "1 mês" : (int)(ts.TotalDays / 30.4368) + " meses";
            //years ago
            return (int)(ts.TotalDays / 365.242) == 1 ? "1 ano" : (int)(ts.TotalDays / 365.242) + " anos";
        }


    }
}
