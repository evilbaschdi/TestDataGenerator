using System;

namespace TestDataGenerator.Internal
{
    /// <inheritdoc />
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TestDataType : ITestDataType
    {
        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public TestDataType(string testDataTypeString)
        {
            Value = testDataTypeString ?? throw new ArgumentNullException(nameof(testDataTypeString));
        }

        /// <inheritdoc />
        public string Value { get; }
    }
}