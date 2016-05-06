using System;

namespace TestDataGenerator.Internal
{
    /// <summary>
    /// </summary>
    public class GetTestDataType : ITestDataType
    {
        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
        public GetTestDataType(string testDataTypeString)
        {
            if(testDataTypeString == null)
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