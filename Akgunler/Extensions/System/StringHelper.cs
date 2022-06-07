using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Akgunler.Extensions
{
    public static class StringHelper
    {
        public static string ToUrlFriendly(this string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }
            name = name.ToLower();
            name = RemoveDiacritics(name);
            name = ConvertEdgeCases(name);
            name = name.Replace(" ", "-");
            name = name.Strip(c =>
                c != '-'
                && c != '_'
                && !c.IsLetter()
                && !Char.IsDigit(c)
                );

            while (name.Contains("--"))
                name = name.Replace("--", "-");

            if (name.Length > 200)
                name = name.Substring(0, 200);

            return name;
        }

        public static string ToLatinString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            value = value.Trim();
            value = ToUrlFriendly(value);

            while (value.Contains("-"))
                value = value.Replace("-", " ");

            value = value.ToUpper().Replace("İ", "I");

            return value;
        }

        public static string ToLatinTitleString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            value = value.ToLatinString();
            value = string.Join(' ', value.Split(' ').Select(x => x.Substring(0, 1).ToUpper() + x.Substring(1).ToLower()));

            return value;
        }

        public static string ToSmsString(this string value)
        {
            value = value.Replace("ö", "o");
            value = value.Replace("ü", "u");
            value = value.Replace("ç", "c");
            value = value.Replace("ş", "s");
            value = value.Replace("ğ", "g");
            value = value.Replace("ı", "i");
            value = value.Replace("Ö", "Ö");
            value = value.Replace("Ü", "Ü");
            value = value.Replace("Ç", "Ç");
            value = value.Replace("Ş", "S");
            value = value.Replace("İ", "I");
            value = value.Replace("Ğ", "G");
            return value;
        }

        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static bool IsLetter(this char c)
        {
            return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z');
        }

        public static bool IsSpace(this char c)
        {
            return (c == '\r' || c == '\n' || c == '\t' || c == '\f' || c == ' ');
        }

        public static string Strip(this string subject, params char[] stripped)
        {
            if (stripped == null || stripped.Length == 0 || String.IsNullOrEmpty(subject))
            {
                return subject;
            }

            var result = new char[subject.Length];

            var cursor = 0;
            for (var i = 0; i < subject.Length; i++)
            {
                char current = subject[i];
                if (Array.IndexOf(stripped, current) < 0)
                {
                    result[cursor++] = current;
                }
            }

            return new string(result, 0, cursor);
        }

        public static string Strip(this string subject, Func<char, bool> predicate)
        {

            var result = new char[subject.Length];

            var cursor = 0;
            for (var i = 0; i < subject.Length; i++)
            {
                char current = subject[i];
                if (!predicate(current))
                {
                    result[cursor++] = current;
                }
            }

            return new string(result, 0, cursor);
        }

        private static string ConvertEdgeCases(string text)
        {
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                sb.Append(ConvertEdgeCases(c));
            }

            return sb.ToString();
        }

        private static string ConvertEdgeCases(char c)
        {
            string swap;
            switch (c)
            {
                case 'ı':
                    swap = "i";
                    break;
                case 'ğ':
                    swap = "g";
                    break;
                case 'ş':
                    swap = "s";
                    break;
                case 'ü':
                    swap = "u";
                    break;
                case 'ö':
                    swap = "o";
                    break;
                case 'ç':
                    swap = "c";
                    break;
                case 'ł':
                    swap = "l";
                    break;
                case 'Ł':
                    swap = "l";
                    break;
                case 'đ':
                    swap = "d";
                    break;
                case 'ß':
                    swap = "ss";
                    break;
                case 'ø':
                    swap = "o";
                    break;
                case 'Þ':
                    swap = "th";
                    break;
                default:
                    swap = c.ToString();
                    break;
            }

            return swap;
        }

        public static string HashWithSignature(this string hashString, string signature)
        {
            var binaryHash = new HMACMD5(Encoding.UTF8.GetBytes(signature))
                .ComputeHash(Encoding.UTF8.GetBytes(hashString));

            var hash = BitConverter.ToString(binaryHash)
                .Replace("-", string.Empty)
                    .ToLowerInvariant();

            return hash;
        }

        public static string EncryptText(this string input, string password)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public static string DecryptText(this string input, string password)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static string ReplaceLastOccurrence(this string source, string find, string replace)
        {
            int place = source.LastIndexOf(find);

            if (place == -1)
                return source;

            string result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }

        public static string ToCleanPhoneNumber(this string phone, bool clearFirstZero = false, string prefix = "")
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return string.Empty;
            }

            string result;
            result = Regex.Replace(phone, @" ", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");

            if(clearFirstZero)
            {
               result = result.TrimStart('0');
            }

            if(!string.IsNullOrEmpty(prefix))
            {
                result = prefix + result;
            }

            return result;

        }

        public static string ToInputPhoneNumber(this string phone, bool addFirstZero = false, string removePrefix = "")
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return string.Empty;
            }

            string result;
            result = Regex.Replace(phone, @" ", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");

            if (phone.StartsWith(removePrefix))
            {
                result = result.Substring(removePrefix.Length);
            }

            if (addFirstZero && !phone.StartsWith("0"))
            {
                result = "0" + result;
            }

            return result;

        }

        public static string PlaceSuffix(this string value, bool quotes = true)
        {
            var lastVowel = 'x';
            var vowelChars = new char[] { 'a', 'o', 'ı', 'u', 'e', 'i', 'ü', 'ö' };
            var boldVowelChars = new char[] { 'a', 'o', 'ı', 'u' };
            var thinVowelChars = new char[] { 'e', 'i', 'ü', 'ö' };

            var quotesStr = quotes ? "'" : "";

            var lastChar = value.ToLower().ToCharArray().Last();
            var word = value + quotesStr;

            var vowels = "OoUuÖöAaOoEeıIiİÜü".ToCharArray();

            if (value != null)
            {
                foreach (char c in value.ToLower())
                {
                    if (vowels.Contains(c))
                    {
                        lastVowel = c;
                    }
                }
            }

            var nChar = vowelChars.Any(x => lastChar.ToString().EndsWith(x)) ? "n" : "";
            var consonantChar = (new char[] { 'ç', 'f', 'h', 'k', 's', 'ş', 't', 'p' }).Contains(lastChar) ? 't' : 'd';
            var vowelChar = thinVowelChars.Contains(lastVowel) ? 'e' : 'a';
            return word + nChar + consonantChar + vowelChar;
        }
    }
}
