using System;

namespace TestDataGenerator.Internal
{
    public class GetTestData : ITestData
    {
        private readonly ITestDataLengh _testDataLengh;
        private readonly ITestDataType _testDataType;

        private const string CapitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string SmallLetters = "abcdefghijklmnopqrstuvwyxz";
        private const string Numbers = "1234567890";
        private const string Signs = @"@€!§$%&/(){}[]\=?+*~#,;.:";

        private static readonly Random Rng = new Random();

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public GetTestData(ITestDataLengh testDataLengh, ITestDataType testDataType)
        {
            if (testDataLengh == null)
            {
                throw new ArgumentNullException("testDataLengh");
            }
            if (testDataType == null)
            {
                throw new ArgumentNullException("testDataType");
            }
            _testDataLengh = testDataLengh;
            _testDataType = testDataType;
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

        public string Value
        {
            get
            {
                switch (_testDataType.Value)
                {
                    case "Letters":
                        return GenerateTestData(_testDataLengh.Value, CapitalLetters + SmallLetters);

                    case "Numbers":
                        return GenerateTestData(_testDataLengh.Value, Numbers);

                    case "Capital Letters":
                        return GenerateTestData(_testDataLengh.Value, CapitalLetters);

                    case "Small Letters":
                        return GenerateTestData(_testDataLengh.Value, SmallLetters);

                    case "Letters and Numbers":
                        return GenerateTestData(_testDataLengh.Value,
                            string.Format("{0}{1}{2}", CapitalLetters, SmallLetters, Numbers));

                    case "Letters, Numbers and Signs":
                        return GenerateTestData(_testDataLengh.Value,
                            string.Format("{0}{1}{2}{3}", CapitalLetters, SmallLetters, Numbers, Signs));

                    default:
                        return GenerateTestData(_testDataLengh.Value,
                            string.Format("{0}{1}{2}", CapitalLetters, SmallLetters, Numbers));
                }
            }
        }
    }
}