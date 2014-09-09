using System;

namespace TestDataGenerator.Internal
{
    internal class TestData
    {
        private const string CapitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string SmallLetters = "abcdefghijklmnopqrstuvwyxz";
        private const string Numbers = "1234567890";
        private const string Signs = @"@€!§$%&/(){}[]\=?+*~#,;.:";

        private static readonly Random Rng = new Random();

        public static string DataType { get; set; }

        public static int DataLength { get; set; }

        public static string Output { get; set; }

        public static void Generate()
        {
            switch (DataType)
            {
                case "Letters":
                    Output = GenerateTestData(DataLength, CapitalLetters + SmallLetters);
                    break;

                case "Numbers":
                    Output = GenerateTestData(DataLength, Numbers);
                    break;

                case "Capital Letters":
                    Output = GenerateTestData(DataLength, CapitalLetters);
                    break;

                case "Small Letters":
                    Output = GenerateTestData(DataLength, SmallLetters);
                    break;

                case "Letters and Numbers":
                    Output = GenerateTestData(DataLength,
                        string.Format("{0}{1}{2}", CapitalLetters, SmallLetters, Numbers));
                    break;

                case "Letters, Numbers and Signs":
                    Output = GenerateTestData(DataLength,
                        string.Format("{0}{1}{2}{3}", CapitalLetters, SmallLetters, Numbers, Signs));
                    break;

                default:
                    Output = GenerateTestData(DataLength,
                        string.Format("{0}{1}{2}", CapitalLetters, SmallLetters, Numbers));
                    break;
            }
        }

        private static string GenerateTestData(int length, string charPool)
        {
            var buffer = new char[length];

            for (var i = 0; i < length; i++)
            {
                buffer[i] = charPool[Rng.Next(charPool.Length)];
            }
            return new string(buffer);
        }
    }
}