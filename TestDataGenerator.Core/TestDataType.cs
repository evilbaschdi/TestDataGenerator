namespace TestDataGenerator.Core;

/// <inheritdoc />
// ReSharper disable once ClassNeverInstantiated.Global
public class TestDataType : ITestDataType
{
    /// <summary>
    ///     Gets / Sets TestDataType
    /// </summary>
    public string Value { get; set; } = "Letters";
}

/// <inheritdoc />
public interface ITestDataTypeCollection : IValueOfList<string>
{
}

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