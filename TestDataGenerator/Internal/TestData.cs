using System;

namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public class TestData : ITestData
    {
        private readonly ITestDataLength _testDataLength;
        private readonly ITestDataType _testDataType;
        private readonly ITestDataCharPool _testDataCharPool;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public TestData(ITestDataLength testDataLength, ITestDataType testDataType, ITestDataCharPool testDataCharPool)
        {
            if (testDataLength == null)
            {
                throw new ArgumentNullException(nameof(testDataLength));
            }
            if (testDataType == null)
            {
                throw new ArgumentNullException(nameof(testDataType));
            }
            if (testDataCharPool == null)
            {
                throw new ArgumentNullException(nameof(testDataCharPool));
            }
            _testDataLength = testDataLength;
            _testDataType = testDataType;
            _testDataCharPool = testDataCharPool;
        }

        /// <summary>
        /// </summary>
        public string Value
        {
            get
            {
                switch (_testDataType.Value)
                {
                    case "Letters":
                        return new GenerateTestData(_testDataLength.Value, _testDataCharPool.Letters).Value;

                    case "Numbers":
                        return new GenerateTestData(_testDataLength.Value, _testDataCharPool.Numbers).Value;

                    case "Capital Letters":
                        return new GenerateTestData(_testDataLength.Value, _testDataCharPool.CapitalLetters).Value;

                    case "Small Letters":
                        return new GenerateTestData(_testDataLength.Value, _testDataCharPool.SmallLetters).Value;

                    case "Letters and Numbers":
                        return new GenerateTestData(_testDataLength.Value, _testDataCharPool.LettersNumbers).Value;

                    case "Letters, Numbers and Signs":
                        return new GenerateTestData(_testDataLength.Value, _testDataCharPool.LettersNumbersSigns).Value;

                    case "Guids (digits)":
                        return new GenerateTestGuids(_testDataLength.Value, "N").Value;

                    case "Guids (hyphens)":
                        return new GenerateTestGuids(_testDataLength.Value, "D").Value;

                    case "Guids (braces)":
                        return new GenerateTestGuids(_testDataLength.Value, "B").Value;

                    case "Guids (parentheses)":
                        return new GenerateTestGuids(_testDataLength.Value, "P").Value;

                    default:
                        return new GenerateTestData(_testDataLength.Value, _testDataCharPool.LettersNumbersSigns).Value;
                }
            }
        }
    }
}