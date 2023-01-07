using JetBrains.Annotations;

namespace TestDataGenerator.Internal;

/// <inheritdoc />
// ReSharper disable once ClassNeverInstantiated.Global
public class TestDataType : ITestDataType
{
    /// <summary>
    ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public TestDataType([NotNull] string testDataTypeString)
    {
        if (testDataTypeString == null)
        {
            throw new ArgumentNullException(nameof(testDataTypeString));
        }

        Value = testDataTypeString ?? throw new ArgumentNullException(nameof(testDataTypeString));
    }

    /// <inheritdoc />
    public string Value { get; }
}