using System;

namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public class TestDataType : ITestDataType
    {
        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public TestDataType(string testDataTypeString)
        {
            if (testDataTypeString == null)
            {
                throw new ArgumentNullException(nameof(testDataTypeString));
            }
            Value = testDataTypeString;
        }

        /// <summary>
        /// </summary>
        public string Value { get; }
    }
}