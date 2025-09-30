namespace dev_repmove_autotest.Utils.Helper
{
    public class Generator
    {
        private static readonly Random random = new Random();
        public static string GenerateRandomNumbers(int lenght)
        {
            var numbers = new int[lenght];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(0, 10);
            }

            return string.Join("", numbers);
        }
        public static string GenerateRandomPassword(int length)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*";
            const string all = upper + lower + digits + special;

            if (length < 4)
                throw new ArgumentException("Password length must be at least 4 to include all required character types.");

            var random = new Random();
            var passwordChars = new List<char>
            {
                upper[random.Next(upper.Length)],
                lower[random.Next(lower.Length)],
                digits[random.Next(digits.Length)],
                special[random.Next(special.Length)]
            };

            for (int i = 4; i < length; i++)
            {
                passwordChars.Add(all[random.Next(all.Length)]);
            }

            return new string(passwordChars.OrderBy(x => random.Next()).ToArray());
        }


        public static string GenerateRandomEmail()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            const string domains = "gmail.com,yahoo.com,outlook.com,hotmail.com,ukr.net";
            var domainArray = domains.Split(',');

            var usernameLength = random.Next(5, 11);
            var username = new char[usernameLength];
            for (int i = 0; i < usernameLength; i++)
            {
                username[i] = chars[random.Next(chars.Length)];
            }
            var domain = domainArray[random.Next(domainArray.Length)];

            return $"{new string(username)}@{domain}";
        }
    }
}
