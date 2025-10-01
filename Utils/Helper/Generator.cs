using dev_repmove_autotest.Utils.Enum;
using System.Security.Cryptography;
using System.Text;

namespace dev_repmove_autotest.Utils.Helper
{
    public static class Generator
    {
        private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lower = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string Special = "!@#$%^&*";
        private static readonly string AllChars = Upper + Lower + Digits + Special;

        private static readonly string[] EmailDomains = { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "ukr.net" };

        private static readonly string[] UkraineMobileCodes =
            { "50","63","66","67","68","73","91","92","93","94","95","96","97","98","99" };

        private static int GetRandomInt(int maxExclusive) => RandomNumberGenerator.GetInt32(maxExclusive);
        private static int GetRandomInt(int minInclusive, int maxExclusive) => RandomNumberGenerator.GetInt32(minInclusive, maxExclusive);

        private static char GetRandomCharFrom(string source)
        {
            return source[GetRandomInt(source.Length)];
        }

        private static string GetRandomDigits(int count)
        {
            var sb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
                sb.Append((char)('0' + GetRandomInt(10)));
            return sb.ToString();
        }

        private static void ShuffleInPlace<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = GetRandomInt(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }


        public static string GenerateRandomString(int length)
        {
            if (length <= 0) throw new ArgumentException("Length must be positive", nameof(length));
            const string chars = Upper + Lower;
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                sb.Append(GetRandomCharFrom(chars));
            return sb.ToString();
        }

        public static string GenerateRandomPassword(int length)
        {
            if (length < 4)
                throw new ArgumentException("Password length must be at least 4 to include all required character types.", nameof(length));

            var chars = new List<char>
            {
                GetRandomCharFrom(Upper),
                GetRandomCharFrom(Lower),
                GetRandomCharFrom(Digits),
                GetRandomCharFrom(Special)
            };

            for (int i = 4; i < length; i++)
                chars.Add(GetRandomCharFrom(AllChars));

            ShuffleInPlace(chars);
            return new string(chars.ToArray());
        }


        public static string GenerateRandomEmail()
        {
            var usernameLength = GetRandomInt(5, 11); 
            var sb = new StringBuilder(usernameLength);
            const string usernameChars = Lower + Digits;

            sb.Append(GetRandomCharFrom(usernameChars));
            for (int i = 1; i < usernameLength - 1; i++)
                sb.Append(GetRandomCharFrom(usernameChars));
            if (usernameLength > 1)
                sb.Append(GetRandomCharFrom(usernameChars));

            var domain = EmailDomains[GetRandomInt(EmailDomains.Length)];
            return $"{sb}@{domain}";
        }

        public static string GenerateValidPhoneNumber(ECountryCode countryCode, bool includeCountryPrefix = false)
        {
            string national = countryCode switch
            {
                ECountryCode.GreatBritan => GenerateUKDomesticNumber(), 
                ECountryCode.Usa => GenerateUSADomesticNumber(),       
                ECountryCode.Canada => GenerateCanadaDomesticNumber(), 
                ECountryCode.Ukraine => GenerateUkraineDomesticNumber(),
                _ => GetRandomDigits(10)
            };

            if (!includeCountryPrefix) return national;

            return countryCode switch
            {
                ECountryCode.GreatBritan => $"+44{national.Substring(1)}", 
                ECountryCode.Usa => $"+1{national}",
                ECountryCode.Canada => $"+1{national}",
                ECountryCode.Ukraine => $"+380{national}",
                _ => national
            };
        }
        private static string GenerateUkraineDomesticNumber()
        {
            var code = UkraineMobileCodes[GetRandomInt(UkraineMobileCodes.Length)];
            var subscriber = GetRandomDigits(7);
            return $"{code}{subscriber}";
        }

        private static string GenerateUKDomesticNumber()
        {
            var after7 = GetRandomDigits(9); 
            return $"07{after7}";
        }


        private static string GenerateUSADomesticNumber()
        {
            var usaAreaCodes = new[] { "212", "213", "214", "305", "312", "404", "415", "512", "602", "702", "718", "818", "917" };
            var area = usaAreaCodes[GetRandomInt(usaAreaCodes.Length)];

            int centralFirst = GetRandomInt(8) + 2;
            int centralSecond = GetRandomInt(10);
            int centralThird = GetRandomInt(10);
            var central = $"{centralFirst}{centralSecond}{centralThird}";

            var subscriber = GetRandomDigits(4);

            return $"{area}{central}{subscriber}";
        }

        private static string GenerateCanadaDomesticNumber()
        {
            var canadaAreaCodes = new[] { "506", "514", "519", "548", "579", "581", "587" };
            var area = canadaAreaCodes[GetRandomInt(canadaAreaCodes.Length)];
            
            int centralFirst = GetRandomInt(8) + 2;
            int centralSecond = GetRandomInt(10);
            int centralThird = GetRandomInt(10);
            var central = $"{centralFirst}{centralSecond}{centralThird}";

            var subscriber = GetRandomDigits(4);

            return $"{area}{central}{subscriber}";
        }
    }
}
