using System;

namespace TestDataGenerator.Internal
{
    public class GetTestData : ITestData
    {
        private readonly ITestDataLengh _testDataLengh;
        private readonly ITestDataType _testDataType;
        private readonly ITestDataCharPool _testDataCharPool;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public GetTestData(ITestDataLengh testDataLengh, ITestDataType testDataType, ITestDataCharPool testDataCharPool)
        {
            if (testDataLengh == null)
            {
                throw new ArgumentNullException("testDataLengh");
            }
            if (testDataType == null)
            {
                throw new ArgumentNullException("testDataType");
            }
            if (testDataCharPool == null)
            {
                throw new ArgumentNullException("testDataCharPool");
            }
            _testDataLengh = testDataLengh;
            _testDataType = testDataType;
            _testDataCharPool = testDataCharPool;
        }

        public string Value
        {
            get
            {
                switch (_testDataType.Value)
                {
                    case "Letters":
                        return new GenerateTestData(_testDataLengh.Value, _testDataCharPool.Letters).Value;

                    case "Numbers":
                        return new GenerateTestData(_testDataLengh.Value, _testDataCharPool.Numbers).Value;

                    case "Capital Letters":
                        return new GenerateTestData(_testDataLengh.Value, _testDataCharPool.CapitalLetters).Value;

                    case "Small Letters":
                        return new GenerateTestData(_testDataLengh.Value, _testDataCharPool.SmallLetters).Value;

                    case "Letters and Numbers":
                        return new GenerateTestData(_testDataLengh.Value, _testDataCharPool.LettersNumbers).Value;

                    case "Letters, Numbers and Signs":
                        return new GenerateTestData(_testDataLengh.Value, _testDataCharPool.LettersNumbersSigns).Value;

                    default:
                        return new GenerateTestData(_testDataLengh.Value, _testDataCharPool.LettersNumbersSigns).Value;
                }
            }
        }
    }
}