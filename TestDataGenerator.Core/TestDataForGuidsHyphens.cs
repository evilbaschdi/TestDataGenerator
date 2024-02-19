namespace TestDataGenerator.Core;

/// <inheritdoc />
public class TestDataForGuidsHyphens : ChainHelperFor<string, string>
{
    private readonly IGenerateTestGuids _generateTestGuids;

    /// <inheritdoc />
    /// <summary>
    ///     Constructor of the class
    /// </summary>
    /// <param name="chainHelperFor"></param>
    /// <param name="generateTestGuids"></param>
    public TestDataForGuidsHyphens(IChainHelperFor<string, string> chainHelperFor, IGenerateTestGuids generateTestGuids)
        : base(chainHelperFor)
    {
        _generateTestGuids = generateTestGuids ?? throw new ArgumentNullException(nameof(generateTestGuids));
    }

    /// <inheritdoc />
    public override bool AmIResponsible => Input.Equals("Guids (hyphens)");

    /// <inheritdoc />
    protected override string InnerValueFor(string input)
    {
        return _generateTestGuids.ValueFor("D");
    }
}