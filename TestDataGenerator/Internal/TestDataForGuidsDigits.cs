using System;

namespace TestDataGenerator.Internal
{
    /// <inheritdoc />
    public class TestDataForGuidsDigits : ChainHelperFor<string, string>
    {
        private readonly IGenerateTestGuids _generateTestGuids;

        /// <inheritdoc />
        /// <summary>
        ///     Constructor of the class
        /// </summary>
        /// <param name="chainHelperFor"></param>
        /// <param name="generateTestGuids"></param>
        public TestDataForGuidsDigits(IChainHelperFor<string, string> chainHelperFor, IGenerateTestGuids generateTestGuids)
            : base(chainHelperFor)
        {
            _generateTestGuids = generateTestGuids ?? throw new ArgumentNullException(nameof(generateTestGuids));
        }

        /// <inheritdoc />
        public override bool AmIResponsible => Input.Equals("Guids (digits)");

        /// <inheritdoc />
        protected override string InnerValueFor(string input)
        {
            return _generateTestGuids.ValueFor("N");
        }
    }
}