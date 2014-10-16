using System;

namespace TestDataGenerator.Internal
{
    public class GetTestData : ITestData
    {
        private readonly ITestDataLengh _testDataLengh;
        private readonly ITestDataType _testDataType;

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

        public string Value
        {
            get
            {
                switch (_testDataType.Value)
                {
                    case "Letters":
                        return new GenerateTestData(_testDataLengh.Value, TestDataCharPool.Letters).Value;

                    case "Numbers":
                        return new GenerateTestData(_testDataLengh.Value, TestDataCharPool.Numbers).Value;

                    case "Capital Letters":
                        return new GenerateTestData(_testDataLengh.Value, TestDataCharPool.CapitalLetters).Value;

                    case "Small Letters":
                        return new GenerateTestData(_testDataLengh.Value, TestDataCharPool.SmallLetters).Value;

                    case "Letters and Numbers":
                        return new GenerateTestData(_testDataLengh.Value, TestDataCharPool.LettersNumbers).Value;

                    case "Letters, Numbers and Signs":
                        return new GenerateTestData(_testDataLengh.Value, TestDataCharPool.LettersNumbersSigns).Value;

                    default:
                        return new GenerateTestData(_testDataLengh.Value, TestDataCharPool.LettersNumbersSigns).Value;
                }
            }
        }
    }
}