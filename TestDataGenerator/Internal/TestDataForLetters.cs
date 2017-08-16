using System;

namespace TestDataGenerator.Internal
{
    /// <inheritdoc />
    public class TestDataForLetters : ChainHelperFor<string, string>
    {
        private readonly IGenerateTestData _generateTestData;
        private readonly ITestDataCharPool _testDataCharPool;

        /// <inheritdoc />
        /// <summary>
        ///     Constructor of the class
        /// </summary>
        /// <param name="chainHelperFor"></param>
        /// <param name="generateTestData"></param>
        /// <param name="testDataCharPool"></param>
        public TestDataForLetters(IChainHelperFor<string, string> chainHelperFor, IGenerateTestData generateTestData,
                                  ITestDataCharPool testDataCharPool)
            : base(chainHelperFor)
        {
            _testDataCharPool = testDataCharPool ?? throw new ArgumentNullException(nameof(testDataCharPool));
            _generateTestData = generateTestData ?? throw new ArgumentNullException(nameof(generateTestData));
        }

        /// <inheritdoc />
        public override bool AmIResponsible => Input.Equals("Letters");

        /// <inheritdoc />
        protected override string InnerValueFor(string input)
        {
            return _generateTestData.ValueFor(_testDataCharPool.Letters);
        }
    }
}