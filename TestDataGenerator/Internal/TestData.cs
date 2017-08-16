using System;

namespace TestDataGenerator.Internal
{
    /// <inheritdoc />
    public class TestData : ITestData
    {
        private readonly IChainHelperFor<string, string> _chainHelperFor;
        private readonly ITestDataType _testDataType;

        /// <summary>
        ///     Constructor of the class
        /// </summary>
        /// <param name="chainHelperFor"></param>
        /// <param name="testDataType"></param>
        public TestData(IChainHelperFor<string, string> chainHelperFor, ITestDataType testDataType)
        {
            _testDataType = testDataType ?? throw new ArgumentNullException(nameof(testDataType));
            _chainHelperFor = chainHelperFor ?? throw new ArgumentNullException(nameof(chainHelperFor));
        }

        /// <inheritdoc />
        public string Value => _chainHelperFor.ValueFor(_testDataType.Value);
    }
}