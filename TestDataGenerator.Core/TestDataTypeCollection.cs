namespace TestDataGenerator.Core;

/// <inheritdoc />
public class TestDataTypeCollection : ITestDataTypeCollection
{
    /// <inheritdoc />
    public List<string> Value =>
    [
        "Letters",
        "Numbers",
        "Capital Letters",
        "Small Letters",
        "Letters and Numbers",
        "Letters, Numbers and Signs",
        "Guids (digits)",
        "Guids (hyphens)",
        "Guids (braces)",
        "Guids (parentheses)"
    ];
}