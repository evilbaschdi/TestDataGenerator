using EvilBaschdi.Core.Internal.ChainLink;

namespace TestDataGenerator.Core;

/// <inheritdoc cref="ITestDataFor" />
/// <inheritdoc cref="ChainLinkValueFor{T,T}" />
public class TestDataForGuidsHyphens : ChainLinkValueFor<string, string>, ITestDataFor
{
    private readonly ITestDataType _testDataType;
    private readonly IGenerateTestGuids _generateTestGuids;

    /// <inheritdoc />
    /// <summary>
    ///     Constructor of the class
    /// </summary>
    /// <param name="testDataFor"></param>
    /// <param name="testDataType"></param>
    /// <param name="generateTestGuids"></param>
    public TestDataForGuidsHyphens(
        ITestDataFor testDataFor,
        ITestDataType testDataType,
        IGenerateTestGuids generateTestGuids)
        : base(testDataFor)
    {
        _testDataType = testDataType ?? throw new ArgumentNullException(nameof(testDataType));
        _generateTestGuids = generateTestGuids ?? throw new ArgumentNullException(nameof(generateTestGuids));
    }

    /// <inheritdoc />
    public override bool AmIResponsible => _testDataType.Value.Equals("Guids (hyphens)");

    /// <inheritdoc />
    protected override string InnerValueFor(string input) => _generateTestGuids.ValueFor("D");
}