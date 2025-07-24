using EvilBaschdi.Core.Internal.ChainLink;

namespace TestDataGenerator.Core;

/// <inheritdoc cref="ITestDataFor" />
/// <inheritdoc cref="ChainLinkValueFor{T,T}" />
public class TestDataFor : ChainLinkValueFor<string, string>, ITestDataFor
{
    private readonly ITestDataType _testDataType;

    /// <summary>
    ///     Constructor of the class
    /// </summary>
    /// <param name="testDataType"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public TestDataFor(ITestDataType testDataType)
    {
        _testDataType = testDataType ?? throw new ArgumentNullException(nameof(testDataType));
    }

    /// <inheritdoc />
    public override bool AmIResponsible => string.IsNullOrWhiteSpace(_testDataType.Value);

    /// <inheritdoc />
    protected override string InnerValueFor(string input) => string.Empty;
}