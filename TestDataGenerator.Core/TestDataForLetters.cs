using EvilBaschdi.Core.Internal.ChainLink;

namespace TestDataGenerator.Core;

/// <inheritdoc cref="ITestDataFor" />
/// <inheritdoc cref="ChainLinkValueFor{T,T}" />
public class TestDataForLetters : ChainLinkValueFor<string, string>, ITestDataFor
{
    private readonly IGenerateTestData _generateTestData;
    private readonly ITestDataType _testDataType;
    private readonly ITestDataCharPool _testDataCharPool;

    /// <inheritdoc />
    /// <summary>
    ///     Constructor of the class
    /// </summary>
    /// <param name="testDataFor"></param>
    /// <param name="generateTestData"></param>
    /// <param name="testDataType"></param>
    /// <param name="testDataCharPool"></param>
    public TestDataForLetters(
        ITestDataFor testDataFor,
        ITestDataType testDataType,
        IGenerateTestData generateTestData,
        ITestDataCharPool testDataCharPool)
        : base(testDataFor)
    {
        _testDataCharPool = testDataCharPool ?? throw new ArgumentNullException(nameof(testDataCharPool));
        _generateTestData = generateTestData ?? throw new ArgumentNullException(nameof(generateTestData));
        _testDataType = testDataType ?? throw new ArgumentNullException(nameof(testDataType));
    }

    /// <inheritdoc />
    public override bool AmIResponsible => _testDataType.Value.Equals("Letters");

    /// <inheritdoc />
    protected override string InnerValueFor(string input) => _generateTestData.ValueFor(_testDataCharPool.Letters);
}