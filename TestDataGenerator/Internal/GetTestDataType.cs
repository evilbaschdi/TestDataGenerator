using System;

namespace TestDataGenerator.Internal
{
    public class GetTestDataType :ITestDataType
    {
        private readonly string _testDataTypeString;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="T:System.Object"/>-Klasse.
        /// </summary>
        public GetTestDataType(string testDataTypeString)
        {
            if (testDataTypeString == null)
            {
                throw new ArgumentNullException("testDataTypeString");
            }
            _testDataTypeString = testDataTypeString;
        }

        public string Value
        {
            get { return _testDataTypeString; }
        }
    }
}