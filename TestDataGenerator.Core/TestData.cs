namespace TestDataGenerator.Core;

/// <inheritdoc />
public class TestData : ITestData
{
    private readonly ITestDataFor _chainLinkValueFor;
    private readonly ITestDataType _testDataType;

    /// <summary>
    ///     Constructor of the class
    /// </summary>
    /// <param name="chainLinkValueFor"></param>
    /// <param name="testDataType"></param>
    // ReSharper disable once InconsistentNaming
    public TestData(ITestDataFor chainLinkValueFor, ITestDataType testDataType)
    {
        _testDataType = testDataType ?? throw new ArgumentNullException(nameof(testDataType));
        _chainLinkValueFor = chainLinkValueFor ?? throw new ArgumentNullException(nameof(chainLinkValueFor));
    }

    /// <inheritdoc />
    public string Value => _chainLinkValueFor.ValueFor(_testDataType.Value);
}